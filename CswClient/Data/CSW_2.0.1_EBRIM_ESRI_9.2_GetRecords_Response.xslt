<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:csw="http://www.opengis.net/cat/csw" xmlns:rim="urn:oasis:names:tc:ebxml-regrep:xsd:rim:3.0" exclude-result-prefixes="csw rim">
  <xsl:output method="xml" indent="no" encoding="UTF-8"  omit-xml-declaration="yes" />
  <xsl:template match="/">
    <Records>
      <xsl:for-each select="//csw:SearchResults/rim:RegistryObject">
        <Record>
          <ID>
            <xsl:value-of select="@id"/>
          </ID>
          <Title>
            <xsl:value-of select="rim:Name/rim:LocalizedString/@value"/>
          </Title>
          <Abstract>
            <xsl:value-of select="rim:Description/rim:LocalizedString/@value"/>
          </Abstract>
          <Type>
            <xsl:value-of select="rim:Slot[@slotType='urn:x-esri:specification:ServiceType:ArcIMS:Metadata:Dataset:Content' and @name='http://purl.org/dc/elements/1.1/type']/rim:ValueList/rim:Value"/>
          </Type>
        </Record>
      </xsl:for-each>
    </Records>
  </xsl:template>
</xsl:stylesheet>
