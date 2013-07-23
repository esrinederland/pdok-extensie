<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:csw="http://www.opengis.net/cat/csw/2.0.2"
				xmlns:ows="http://www.opengis.net/ows"
                xmlns:rim="urn:oasis:names:tc:ebxml-regrep:xsd:rim:3.0"
                exclude-result-prefixes="csw rim">
  <xsl:output method="text" indent="no" encoding="UTF-8"/>
  
    <xsl:template match="/">
    <xsl:choose>
      <xsl:when test="/ows:ExceptionReport">
        <exception>
          <exceptionText>
            <xsl:for-each select="/ows:ExceptionReport/ows:Exception">
              <xsl:value-of select="ows:ExceptionText"/>
            </xsl:for-each>
          </exceptionText>
        </exception>
      </xsl:when>
      <xsl:otherwise>
        <xsl:apply-templates select="//@accessURI"/>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
  
  <xsl:template match="//@accessURI">
    <xsl:value-of select="."/>
     <xsl:text>&#x2714;</xsl:text>
     urn:x-esri:specification:ServiceType:ArcIMS:Metadata:Server
     <xsl:text>&#x2715;</xsl:text>
  </xsl:template>

</xsl:stylesheet>
