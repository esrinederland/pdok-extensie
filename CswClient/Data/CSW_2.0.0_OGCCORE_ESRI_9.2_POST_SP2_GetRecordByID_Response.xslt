<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" 
                xmlns:csw="http://www.opengis.net/cat/csw"
                xmlns:dct="http://purl.org/dc/terms/"
                exclude-result-prefixes="csw dct">
  <xsl:output method="text" indent="no" encoding="UTF-8"/>
  <xsl:template match="/">
  <xsl:apply-templates select="//csw:Record/dct:references"/>
  </xsl:template>
  
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
