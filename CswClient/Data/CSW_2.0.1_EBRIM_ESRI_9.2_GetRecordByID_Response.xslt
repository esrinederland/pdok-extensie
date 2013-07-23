<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:csw="http://www.opengis.net/cat/csw"
                xmlns:rim="urn:oasis:names:tc:ebxml-regrep:xsd:rim:3.0"
                exclude-result-prefixes="csw rim">
  <xsl:output method="text" indent="no" encoding="UTF-8"/>

  <xsl:template match="/">
    <xsl:apply-templates select ="/csw:GetRecordByIdResponse/rim:Service|/csw:GetRecordByIdResponse/rim:ExtrinsicObject"/>
  </xsl:template>

  <!-- url to metadata XML -->
  <xsl:template match="/csw:GetRecordByIdResponse/rim:Service|/csw:GetRecordByIdResponse/rim:ExtrinsicObject">
    <xsl:value-of select="rim:Slot[@slotType='urn:x-esri:specification:ServiceType:ArcIMS:Metadata:Dataset:Url' and @name='http://purl.org/dc/terms/references']/rim:ValueList/rim:Value"/>
  </xsl:template>
</xsl:stylesheet>
