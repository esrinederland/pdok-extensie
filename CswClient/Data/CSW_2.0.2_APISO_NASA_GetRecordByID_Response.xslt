<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"  xmlns:gmd="http://www.isotc211.org/2005/gmd"
                xmlns:csw="http://www.opengis.net/cat/csw"
                xmlns:gco="http://www.isotc211.org/2005/gco"
                exclude-result-prefixes="csw gco gmd">
  <xsl:output method="text" indent="no" encoding="UTF-8"/>
  <xsl:template match="/">
  <xsl:apply-templates select="//gmd:MD_Distribution/gmd:transferOptions/gmd:MD_DigitalTransferOptions/gmd:onLine/gmd:CI_OnlineResource"/>
  </xsl:template>
  
  <xsl:template match="//gmd:MD_Distribution/gmd:transferOptions/gmd:MD_DigitalTransferOptions/gmd:onLine/gmd:CI_OnlineResource">
     <xsl:if test="./gmd:description/gco:CharacterString='NetcdfSubset' ">
		<xsl:value-of select="./gmd:linkage/gmd:URL"/>
	 </xsl:if>
	 <xsl:if test="./gmd:description/gco:CharacterString='THREDDSCatalog' ">
		<xsl:value-of select="./gmd:linkage/gmd:URL"/>
	 </xsl:if>
  </xsl:template>
</xsl:stylesheet>


