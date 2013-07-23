<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"  xmlns:csw="http://www.opengis.net/cat/csw" xmlns:dct="http://www.purl.org/dc/terms/" xmlns:dc="http://www.purl.org/dc/elements/1.1/" exclude-result-prefixes="csw dc dct">
  <xsl:output method="xml" indent="no"  encoding="UTF-8" omit-xml-declaration="yes" />
  <xsl:template match="/">
    <Records>
      <xsl:for-each select="/csw:GetRecordsResponse/csw:SearchResults/csw:Record">
        <Record>
          <ID>
            <xsl:value-of select="dc:identifier"/>
          </ID>
          <Title>
            <xsl:value-of select="dc:title"/>
          </Title>
          <Abstract>
            <xsl:value-of select="dc:description"/>
          </Abstract>
          <Type>
            <xsl:value-of select="dc:type"/>
          </Type>
        </Record>
      </xsl:for-each>
    </Records>
  </xsl:template>
</xsl:stylesheet>