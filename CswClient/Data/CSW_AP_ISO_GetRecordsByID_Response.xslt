<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"  xmlns:csw="http://www.opengis.net/cat/csw" xmlns:dct="http://purl.org/dc/terms/" xmlns:dc="http://purl.org/dc/elements/1.1/" xmlns:gco="http://www.isotc211.org/2005/gco" exclude-result-prefixes="csw dc dct gco">
  <xsl:output method="xml" indent="no"  encoding="UTF-8" omit-xml-declaration="yes" />
  <!--<xsl:template match="/">
    <Records>
      <xsl:for-each select="//MD_Metadata">
        <Record>
          <ID>
            <xsl:value-of select="fileIdentifier/gco:CharacterString"/>
          </ID>
          <Title>
            <xsl:value-of select="dc:title"/>
          </Title>
          <Abstract>
            <xsl:value-of select="dct:abstract"/>
          </Abstract>
          <Type>
            <xsl:value-of select="dc:type"/>
          </Type>
        </Record>
      </xsl:for-each>

    </Records>
  </xsl:template>-->
  
  
  <xsl:template match="/">
     
          <xsl:apply-templates select="//MD_Metadata/dct:references"/>
     
    </xsl:template>
    
    <xsl:template match="//MD_Metadata/dct:references">
      <xsl:value-of select="."/>
       <xsl:text>&#x2714;</xsl:text>
       <xsl:value-of select="@scheme"/>
       <xsl:text>&#x2715;</xsl:text>
  </xsl:template>
</xsl:stylesheet>
