<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:output method="xml" indent="yes" encoding="UTF-8" omit-xml-declaration="no"/>
	<xsl:template match="/">
		<csw:GetRecords 
			outputFormat="application/xml"
			outputSchema="urn:oasis:names:tc:ebxml-regrep:xsd:rim:3.0"
			version="2.0.2"
			service="CSW-EBRIM"
			resultType="results"
			xmlns:rim="urn:oasis:names:tc:ebxml-regrep:xsd:rim:3.0"
			xmlns:csw="http://www.opengis.net/cat/csw/2.0.2" 
			xmlns:ogc="http://www.opengis.net/ogc" >
			<xsl:attribute name="startPosition"><xsl:value-of select="/GetRecords/StartPosition"/></xsl:attribute>
			<xsl:attribute name="maxRecords"><xsl:value-of select="/GetRecords/MaxRecords"/></xsl:attribute>
			<csw:Query typeNames="Service">
				<csw:ElementSetName typeNames="Service">full</csw:ElementSetName>
				<csw:Constraint version="1.1.0">
				  <ogc:Filter>
						<xsl:choose>
							<xsl:when test="count(/GetRecords/KeyWord) + count(/GetRecords/FromDate) > 1">
								<ogc:And>
									<!-- Key Word search -->
									<xsl:apply-templates select="/GetRecords/KeyWord"/>
									<!-- LiveDataOrMaps search -->
									<!-- xsl:apply-templates select="/GetRecords/LiveDataMap"/ -->
									<!-- Envelope search, e.g. ogc:BBOX -->
									<!-- xsl:apply-templates select="/GetRecords/Envelope"/ -->
								</ogc:And>
							</xsl:when>
							<xsl:otherwise>
								<!-- only one criterion is given, do not include enclosing ogc:And elements -->
								
								<!-- Key Word search -->
								<xsl:apply-templates select="/GetRecords/KeyWord"/>
								<!-- LiveDataOrMaps search -->
								<!-- xsl:apply-templates select="/GetRecords/LiveDataMap"/ -->
								<!-- Envelope search, e.g. ogc:BBOX -->
								<!-- xsl:apply-templates select="/GetRecords/Envelope"/ -->
							</xsl:otherwise>
						</xsl:choose>
				  </ogc:Filter>
				</csw:Constraint>
			</csw:Query>
		</csw:GetRecords>
	</xsl:template>
	<!-- key word search -->
	<xsl:template match="/GetRecords/KeyWord" xmlns:ogc="http://www.opengis.net/ogc">
		<xsl:if test="normalize-space(.)!=''">
			<ogc:PropertyIsLike singleChar="?" wildCard="*" escapeChar="~">
				<ogc:PropertyName>Service/Description/LocalizedString/@value</ogc:PropertyName>
				<ogc:Literal>
					<xsl:value-of select="."/>
				</ogc:Literal>
			</ogc:PropertyIsLike>
		</xsl:if>
	</xsl:template>
</xsl:stylesheet>
