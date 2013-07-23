<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
    <xsl:output method="xml" indent="no" encoding="UTF-8" omit-xml-declaration="yes"/>
    <xsl:template match="/">
        <xsl:element name="csw:GetRecords" use-attribute-sets="GetRecordsAttributes" xmlns:csw="http://www.opengis.net/cat/csw/2.0.2" xmlns:ogc="http://www.opengis.net/ogc" xmlns:dc="http://www.purl.org/dc/elements/1.1/" xmlns:gml="http://www.opengis.net/gml">
            <csw:Query  typeNames="gmd:MD_Metadata">
                <csw:ElementSetName>full</csw:ElementSetName>
                <csw:Constraint version="1.1.0">
                    <ogc:Filter xmlns="http://www.opengis.net/ogc">
                        <xsl:apply-templates select="GetRecords/And|GetRecords/Or|GetRecords/Not"/>
                        <!-- Key Word search in case not in logical group-->
                        <xsl:apply-templates select="GetRecords/KeyWord"/>
                        <!-- Protocol search in case not in logical group -->
                        <xsl:apply-templates select="GetRecords/TypeOfUse"/>
                        <!-- Organisation search in case not in logical group-->
                        <xsl:apply-templates select="GetRecords/Organisation"/>
                        <!-- LiveDataOrMaps search  in case not in logical group -->
                        <xsl:apply-templates select="GetRecords/LiveDataMap"/>
                        <!-- Envelope search, e.g. ogc:BBOX  in case not in logical group--><!--
                        <xsl:apply-templates select="GetRecords/Envelope"/>-->
                    </ogc:Filter>
                </csw:Constraint>
                <ogc:SortBy>
                    <ogc:SortProperty>
                        <ogc:PropertyName>title</ogc:PropertyName>
                        <ogc:SortOrder>ASC</ogc:SortOrder>
                    </ogc:SortProperty>
                </ogc:SortBy>
            </csw:Query>
        </xsl:element>
    </xsl:template>

    <xsl:template match="And|Or|Not" xmlns:ogc="http://www.opengis.net/ogc">
        <xsl:variable name="matchname">
            <xsl:value-of select="local-name()"/>
        </xsl:variable>
        <xsl:element name="ogc:{$matchname}">
            <xsl:apply-templates select="./And|./Or|./Not"/>
            <xsl:apply-templates select="./KeyWord"/>
            <xsl:apply-templates select="./TypeOfUse"/>
            <xsl:apply-templates select="./Organisation"/>
        </xsl:element>
    </xsl:template>
    <!-- key word search -->
    <xsl:template match="KeyWord" xmlns:ogc="http://www.opengis.net/ogc">
        <xsl:if test="normalize-space(.)!=''">
            <ogc:PropertyIsLike wildCard="*" escapeChar="\" singleChar="?">
                <ogc:PropertyName>AnyText</ogc:PropertyName>
                <ogc:Literal>*<xsl:value-of select="."/>*</ogc:Literal>
            </ogc:PropertyIsLike>
        </xsl:if>
    </xsl:template>
    <!-- usage search-->
    <xsl:template match="TypeOfUse" xmlns:ogc="http://www.opengis.net/ogc">
        <xsl:choose>
            <xsl:when test="normalize-space(.)='Raadplegen (WMS)'">
                    <ogc:PropertyIsLike wildCard="*" escapeChar="\" singleChar="?">
                        <ogc:PropertyName>protocol</ogc:PropertyName>
                        <ogc:Literal>*WMS*</ogc:Literal>
                    </ogc:PropertyIsLike>
            </xsl:when>
            <xsl:when test="normalize-space(.)='Achtergrond (WMTS)'">
            <!--<xsl:when test="normalize-space(.)='Achtergrond (TMS)'">-->
                <!--<ogc:Or>-->
                    <ogc:PropertyIsLike wildCard="*" escapeChar="\" singleChar="?">
                        <ogc:PropertyName>protocol</ogc:PropertyName>
                        <ogc:Literal>*WMTS*</ogc:Literal>
                    </ogc:PropertyIsLike>
                    <!--<ogc:PropertyIsLike wildCard="*" escapeChar="\" singleChar="?">
                        <ogc:PropertyName>protocol</ogc:PropertyName>
                        <ogc:Literal>*UKST*</ogc:Literal>
                    </ogc:PropertyIsLike>-->
                <!--</ogc:Or>-->
            </xsl:when>
            <xsl:when test="normalize-space(.)='Analyse/download (WFS, WCS)'">
            <!--<xsl:when test="normalize-space(.)='Analyse/download (WFS)'">-->
                <ogc:Or>
                    <ogc:PropertyIsLike wildCard="*" escapeChar="\" singleChar="?">
                        <ogc:PropertyName>protocol</ogc:PropertyName>
                        <ogc:Literal>*WFS*</ogc:Literal>
                    </ogc:PropertyIsLike>
                    <ogc:PropertyIsLike wildCard="*" escapeChar="\" singleChar="?">
                        <ogc:PropertyName>protocol</ogc:PropertyName>
                        <ogc:Literal>*WCS*</ogc:Literal>
                    </ogc:PropertyIsLike>
                </ogc:Or>
            </xsl:when>
        </xsl:choose>
    </xsl:template>


    <!-- LiveDataOrMaps search -->
    <xsl:template match="LiveDataMap" xmlns:ogc="http://www.opengis.net/ogc">
        <xsl:if test=".='True'">
            <ogc:PropertyIsEqualTo>
                <ogc:PropertyName>Format</ogc:PropertyName>
                <ogc:Literal>liveData</ogc:Literal>
            </ogc:PropertyIsEqualTo>
        </xsl:if>
    </xsl:template>
    <xsl:template match="Organisation" xmlns:ogc="http://www.opengis.net/ogc">
        <xsl:if test="normalize-space(.)!=''">
            <ogc:PropertyIsLike wildCard="*" escapeChar="\" singleChar="?">
                <ogc:PropertyName>organisationName</ogc:PropertyName>
                <ogc:Literal>*<xsl:value-of select="."/>*</ogc:Literal>
            </ogc:PropertyIsLike>
        </xsl:if>
    </xsl:template>

    <!-- envelope search -->
    <xsl:template match="Envelope" xmlns:ogc="http://www.opengis.net/ogc">
        <!-- generate BBOX query if minx, miny, maxx, maxy are provided -->
        <xsl:if test="./MinX and ./MinY and ./MaxX and ./MaxY">
            <ogc:BBOX xmlns:gml="http://www.opengis.net/gml">
                <ogc:PropertyName>iso:BoundingBox</ogc:PropertyName>
                <gml:Box>
                    <gml:coordinates>
                        <xsl:value-of select="MinX"/>,<xsl:value-of select="MinY"/>,<xsl:value-of select="MaxX"/>,<xsl:value-of select="MaxY"/>
                    </gml:coordinates>
                </gml:Box>
            </ogc:BBOX>
        </xsl:if>
    </xsl:template>

    <!--
    <xsl:attribute-set name="GetRecordsAttributes">
		<xsl:attribute name="version">2.0.1</xsl:attribute>
		<xsl:attribute name="service">CSW</xsl:attribute>
		<xsl:attribute name="resultType">RESULTS</xsl:attribute>
		<xsl:attribute name="startPosition"><xsl:value-of select="/GetRecords/StartPosition"/></xsl:attribute>
		<xsl:attribute name="maxRecords"><xsl:value-of select="/GetRecords/MaxRecords"/></xsl:attribute>
		<xsl:attribute name="outputSchema">csw:Record</xsl:attribute>
	</xsl:attribute-set>
    -->
    <xsl:attribute-set name="GetRecordsAttributes">
		<xsl:attribute name="outputSchema">csw:IsoRecord</xsl:attribute>
        <xsl:attribute name="version">2.0.2</xsl:attribute>
        <xsl:attribute name="service">CSW</xsl:attribute>
        <xsl:attribute name="resultType">results</xsl:attribute>
        <xsl:attribute name="startPosition">
            <xsl:value-of select="/GetRecords/StartPosition"/>
        </xsl:attribute>
        <xsl:attribute name="maxRecords">
            <xsl:value-of select="/GetRecords/MaxRecords"/>
        </xsl:attribute>
        <!--<xsl:attribute name="maxRecords">5000</xsl:attribute>-->
    </xsl:attribute-set>
</xsl:stylesheet>
