<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"  xmlns:csw="http://www.opengis.net/cat/csw" xmlns:dct="http://purl.org/dc/terms/" xmlns:dc="http://purl.org/dc/elements/1.1/" xmlns:gco="http://www.isotc211.org/2005/gco" exclude-result-prefixes="csw dc dct gco">
  <xsl:output method="xml" indent="yes"  encoding="UTF-8" omit-xml-declaration="yes" />
  <xsl:template match="/">
    <Records>
      <xsl:for-each select="csw:GetRecordsResponse/SearchResults/MD_Metadata">
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
           <!--  <xsl:text>liveData</xsl:text>-->
          </Type>
           <LowerCorner>
			  <xsl:value-of select="csw:SearchResults/MD_Metadata/identificationInfo/MD_DataIdentification/extent/EX_Extent/geographicElement/EX_GeographicBoundingBox/westBoundLongitude/gco:Decimal"/>
			  <xsl:text> </xsl:text>
			  <xsl:value-of select="csw:SearchResults/MD_Metadata/identificationInfo/MD_DataIdentification/extent/EX_Extent/geographicElement/EX_GeographicBoundingBox/southBoundLatitude/gco:Decimal"/>
	    </LowerCorner>
	    <UpperCorner>
			  <xsl:value-of select="csw:SearchResults/MD_Metadata/identificationInfo/MD_DataIdentification/extent/EX_Extent/geographicElement/EX_GeographicBoundingBox/eastBoundLongitude/gco:Decimal"/>
			  <xsl:text> </xsl:text>
			  <xsl:value-of select="csw:SearchResults/MD_Metadata/identificationInfo/MD_DataIdentification/extent/EX_Extent/geographicElement/EX_GeographicBoundingBox/northBoundLatitude/gco:Decimal"/>
          </UpperCorner>
        </Record>
      </xsl:for-each>
    </Records>
  </xsl:template>
</xsl:stylesheet>
