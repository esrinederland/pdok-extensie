<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:gmd="http://www.isotc211.org/2005/gmd" xmlns:gco="http://www.isotc211.org/2005/gco" xmlns:csw="http://www.opengis.net/cat/csw/2.0.2" xmlns:ows="http://www.opengis.net/ows" xmlns:rim="urn:oasis:names:tc:ebxml-regrep:xsd:rim:3.0" exclude-result-prefixes="csw rim">
	<xsl:output method="text" indent="no" encoding="UTF-8"/>
	<xsl:template match="/">
	<xsl:for-each select="//gmd:onLine">
		<xsl:if test=".//gmd:CI_OnlineResource//gmd:protocol//gco:CharacterString = 'OGC:WMS-1.1.1-http-get-capabilities' ">
			<xsl:value-of select="substring-before(.//gmd:CI_OnlineResource//gmd:linkage//gmd:URL,'?')"/>?<xsl:text>&#x2714;</xsl:text>urn:x-esri:specification:ServiceType:ArcIMS:Metadata:Server<xsl:text>&#x2715;</xsl:text>
		</xsl:if>
		</xsl:for-each>
	</xsl:template>
</xsl:stylesheet>
