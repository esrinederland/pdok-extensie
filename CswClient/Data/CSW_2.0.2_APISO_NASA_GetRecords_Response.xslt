<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:csw="http://www.opengis.net/cat/csw/2.0.2" xmlns:gco="http://www.isotc211.org/2005/gco" xmlns:gmd="http://www.isotc211.org/2005/gmd" exclude-result-prefixes="csw gco gmd">
	<xsl:output method="xml" indent="no" encoding="UTF-8" omit-xml-declaration="yes"/>
	<xsl:template match="/">
		<Records>
	<xsl:for-each select="//gmd:MD_Metadata">
				<Record>
					<ID>
						<xsl:value-of select="gmd:fileIdentifier/gco:CharacterString"/>
					</ID>
					<Title>
						<xsl:value-of select="gmd:identificationInfo/gmd:MD_DataIdentification/gmd:citation/gmd:CI_Citation/gmd:title/gco:CharacterString"/>
					</Title>
					<Abstract>
						<xsl:value-of select="gmd:identificationInfo/gmd:MD_DataIdentification/gmd:abstract/gco:CharacterString"/>
					</Abstract>
					<Type>
						<xsl:value-of select="//gmd:MD_ScopeCode/@codeListValue"/>
					</Type>
				</Record>
			</xsl:for-each> 			
		</Records>
	</xsl:template>
</xsl:stylesheet>
