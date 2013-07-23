<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"  xmlns:csw="http://www.opengis.net/cat/csw" xmlns:gco="http://www.isotc211.org/2005/gco" xmlns:gmd="http://www.isotc211.org/2005/gmd" xmlns:srv="http://www.isotc211.org/2005/srv" exclude-result-prefixes="gmd csw gco srv">
  <xsl:output method="xml" indent="yes"  encoding="UTF-8" omit-xml-declaration="yes" />
  <xsl:template match="/">
    <Records>
      <xsl:for-each select="//gmd:MD_Metadata">
        <Record>
          <ID>
            <xsl:value-of select="gmd:fileIdentifier/gco:CharacterString"/>
          </ID>
          <Title>
            <xsl:value-of select="gmd:identificationInfo/srv:SV_ServiceIdentification/gmd:citation/gmd:CI_Citation/gmd:title/gco:CharacterString"/>
          </Title>
          <Abstract>
            <xsl:value-of select="gmd:identificationInfo/srv:SV_ServiceIdentification/gmd:abstract/gco:CharacterString"/>
          </Abstract>
          <Type>
          <!--  <xsl:value-of select="gmd:identificationInfo/srv:SV_ServiceIdentification/srv:serviceType/gco:LocalName"/><xsl:text> </xsl:text><xsl:value-of select="gmd:identificationInfo/srv:SV_ServiceIdentification/srv:serviceTypeVersion/gco:CharacterString"/> -->
            <xsl:text>liveData</xsl:text>
          </Type>
          <LowerCorner>
			  <xsl:value-of select="gmd:identificationInfo/srv:SV_ServiceIdentification/srv:extent/gmd:EX_Extent/gmd:geographicElement/gmd:EX_GeographicBoundingBox/gmd:westBoundLongitude/gco:Decimal"/>
			  <xsl:text> </xsl:text>
			  <xsl:value-of select="gmd:identificationInfo/srv:SV_ServiceIdentification/srv:extent/gmd:EX_Extent/gmd:geographicElement/gmd:EX_GeographicBoundingBox/gmd:southBoundLatitude/gco:Decimal"/>
          </LowerCorner>
          <UpperCorner>
			  <xsl:value-of select="gmd:identificationInfo/srv:SV_ServiceIdentification/srv:extent/gmd:EX_Extent/gmd:geographicElement/gmd:EX_GeographicBoundingBox/gmd:eastBoundLongitude/gco:Decimal"/>
			  <xsl:text> </xsl:text>
			  <xsl:value-of select="gmd:identificationInfo/srv:SV_ServiceIdentification/srv:extent/gmd:EX_Extent/gmd:geographicElement/gmd:EX_GeographicBoundingBox/gmd:northBoundLatitude/gco:Decimal"/>
          </UpperCorner>
        </Record>
      </xsl:for-each>
    </Records>
  </xsl:template>
</xsl:stylesheet>
