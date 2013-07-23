<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:csw="http://www.opengis.net/cat/csw" xmlns:dct="http://purl.org/dc/terms/" xmlns:cat="http://www.esri.com/metadata/csw/" exclude-result-prefixes="csw dct">
	<xsl:output method="text" indent="no" encoding="UTF-8"/>
	<xsl:template match="/">
		<xsl:choose>
			<xsl:when test="count(//csw:Record/dct:references)>0">
				<xsl:apply-templates select="//csw:Record/dct:references"/>
			</xsl:when>
			<xsl:when test="count(//metadata)>0">
				<xsl:copy-of select="//metadata" />
			</xsl:when> 

			<xsl:otherwise>NOTHING FOUND</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	
	<!-- return URL to xml doc included in dct:references as pointer to metadata -->
	<xsl:template match="//csw:Record/dct:references">
		<xsl:for-each select=".">
			<xsl:choose>
				<xsl:when test="contains(., '.xml')">
					<xsl:value-of select="."/>
				</xsl:when>
			</xsl:choose>
		</xsl:for-each>
	</xsl:template>
</xsl:stylesheet>
