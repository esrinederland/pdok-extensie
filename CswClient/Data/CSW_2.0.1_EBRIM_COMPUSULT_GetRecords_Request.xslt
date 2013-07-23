<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:gml="http://www.opengis.net/gml">
	<xsl:output method="xml" indent="yes" encoding="UTF-8" omit-xml-declaration="no"/>
	<xsl:template match="/">
		<xsl:element name="csw:GetRecords" use-attribute-sets="GetRecordsAttributes" xmlns:csw="http://www.opengis.net/cat/csw" xmlns:ogc="http://www.opengis.net/ogc" xmlns:dc="http://purl.org/dc/elements/1.1/" xmlns:ebrim="urn:oasis:names:tc:ebxml-regrep:rim:xsd:2.5">
			<csw:Query typeNames="ExtrinsicObject:$eo CSWExtrinsicObject:$dd Association:$assoc">
				<csw:ElementSetName typeNames="eo,dd,assoc">full</csw:ElementSetName>
				<csw:Constraint version="1.0.0">
					<ogc:Filter xmlns="http://www.opengis.net/ogc">
						<ogc:And>
							<!-- association: ExtrensicObject Describes CSWExtrinsicObject -->
							<ogc:PropertyIsEqualTo>
								<ogc:PropertyName>/$dd/@id</ogc:PropertyName>
								<ogc:PropertyName>/$assoc/@sourceObject</ogc:PropertyName>
							</ogc:PropertyIsEqualTo>
							<ogc:PropertyIsEqualTo>
								<ogc:PropertyName>/$assoc/@associationType</ogc:PropertyName>
								<ogc:Literal>Describes</ogc:Literal>
							</ogc:PropertyIsEqualTo>
							<ogc:PropertyIsEqualTo>
								<ogc:PropertyName>/$eo/@id</ogc:PropertyName>
								<ogc:PropertyName>/$assoc/@targetObject</ogc:PropertyName>
							</ogc:PropertyIsEqualTo>
							<!-- Key Word search -->
							<xsl:apply-templates select="/GetRecords/KeyWord"/>
							<!-- Envelope search, e.g. ogc:BBOX -->
							<xsl:apply-templates select="/GetRecords/Envelope"/>
						</ogc:And>
					</ogc:Filter>
				</csw:Constraint>
			</csw:Query>
		</xsl:element>
	</xsl:template>
	<!-- key word search -->
	<xsl:template match="/GetRecords/KeyWord" xmlns:ogc="http://www.opengis.net/ogc">
		<xsl:if test="normalize-space(.)!=''">
			<ogc:PropertyIsLike wildCard="" escape="" singleChar="">
				<ogc:PropertyName>/$dd/Slot[@name="Keyword"]/ValueList/Value</ogc:PropertyName>
				<ogc:Literal>
					<xsl:value-of select="."/>
				</ogc:Literal>
			</ogc:PropertyIsLike>
		</xsl:if>
	</xsl:template>
	<!-- envelope search -->
	<xsl:template match="/GetRecords/Envelope" xmlns:ogc="http://www.opengis.net/ogc">
		<!-- generate BBOX query if minx, miny, maxx, maxy are provided -->
		<xsl:if test="./MinX and ./MinY and ./MaxX and ./MaxY">
			<ogc:Contains>
				<ogc:PropertyName>/$dd/Slot[@name='FootPrint']/ValueList/Value</ogc:PropertyName>
				<gml:Box srsName="EPSG:4326">
					<gml:coordinates>
						<xsl:value-of select="MaxY"/>,<xsl:value-of select="MinX"/>,<xsl:value-of select="MinY"/>,<xsl:value-of select="MaxX"/>
					</gml:coordinates>
				</gml:Box>
			</ogc:Contains>
		</xsl:if>
	</xsl:template>
	<xsl:attribute-set name="GetRecordsAttributes">
		<xsl:attribute name="version">2.0.1</xsl:attribute>
		<xsl:attribute name="service">CSW</xsl:attribute>
		<xsl:attribute name="resultType">RESULTS</xsl:attribute>
		<xsl:attribute name="startPosition"><xsl:value-of select="/GetRecords/StartPosition"/></xsl:attribute>
		<xsl:attribute name="maxRecords"><xsl:value-of select="/GetRecords/MaxRecords"/></xsl:attribute>
		<xsl:attribute name="outputFormat">application/xml</xsl:attribute>
		<xsl:attribute name="outputSchema">EBRIM</xsl:attribute>
	</xsl:attribute-set>
</xsl:stylesheet>
