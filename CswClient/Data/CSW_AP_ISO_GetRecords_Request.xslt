<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" 
	xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
	xmlns:csw="http://www.opengis.net/cat/csw/2.0.2"
	xmlns:ogc="http://www.opengis.net/ogc"
	xmlns:gmd="http://www.isotc211.org/2005/gmd"
	xmlns:apiso="http://www.opengis.net/cat/csw/apiso/1.0"
	xmlns:ows="http://www.opengis.net/ows"
	xmlns:xsd="http://www.w3.org/2001/XMLSchema"
	xmlns:dc="http://purl.org/dc/elements/1.1/"
	xmlns:dct="http://purl.org/dc/terms/" xmlns:gml="http://www.opengis.net/gml"
	xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">

	<xsl:output method="xml" indent="yes" encoding="UTF-8" omit-xml-declaration="no"/>

	<xsl:template match="/">
		<csw:GetRecords 			 
			service="CSW" 
			version="2.0.2" 
			resultType="results" 
			outputFormat="application/xml" 
			outputSchema="http://www.isotc211.org/2005/gmd">
			
			<xsl:attribute name="startPosition"><xsl:value-of select="/GetRecords/StartPosition"/></xsl:attribute>
			<xsl:attribute name="maxRecords"><xsl:value-of select="/GetRecords/MaxRecords"/></xsl:attribute>
			
			<csw:Query typeNames="gmd:MD_Metadata">
				<csw:ElementSetName>brief</csw:ElementSetName>
				<csw:Constraint version="1.1.0">
					<ogc:Filter>
						<ogc:And>
							<xsl:apply-templates select="/GetRecords/KeyWord"/>
							<xsl:apply-templates select="/GetRecords/Envelope"/>
						</ogc:And>
					</ogc:Filter>
				</csw:Constraint>
			</csw:Query>
		</csw:GetRecords>
	</xsl:template>

	<!-- key word search -->
	<xsl:template match="/GetRecords/KeyWord">
		<xsl:if test="normalize-space(.)!=''">
			<ogc:PropertyIsLike wildCard="*" escapeChar="\" singleChar="?">
				<ogc:PropertyName>apiso:AnyText</ogc:PropertyName>
				<ogc:Literal>*<xsl:value-of select="."/>*</ogc:Literal>
			</ogc:PropertyIsLike>
		</xsl:if>
	</xsl:template>

	<!-- envelope search.-->
	<xsl:template match="/GetRecords/Envelope">
		<!-- generate BBOX query if minx, miny, maxx, maxy are provided -->
		<xsl:if test="./MinX and ./MinY and ./MaxX and ./MaxY">
			<ogc:BBOX xmlns:gml="http://www.opengis.net/gml">
				<ogc:PropertyName>apiso:Geometry</ogc:PropertyName>
				<gml:Envelope srsName="http://www.opengis.net/gml/srs/epsg.xml#63266405">
					<gml:lowerCorner><xsl:value-of select="MinX"/>,<xsl:value-of select="MinY"/></gml:lowerCorner>
					<gml:upperCorner><xsl:value-of select="MaxX"/>,<xsl:value-of select="MaxY"/></gml:upperCorner>					
				</gml:Envelope>
			</ogc:BBOX>
		</xsl:if>
	</xsl:template>

</xsl:stylesheet>