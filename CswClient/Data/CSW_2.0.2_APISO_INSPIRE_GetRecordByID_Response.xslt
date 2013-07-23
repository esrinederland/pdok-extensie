<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:csw="http://www.opengis.net/cat/csw" xmlns:gco="http://www.isotc211.org/2005/gco" xmlns:gmd="http://www.isotc211.org/2005/gmd" xmlns:srv="http://www.isotc211.org/2005/srv" exclude-result-prefixes="">
	<xsl:output method="xml" indent="no" encoding="UTF-8"/>
	
	<!--<xsl:template match="/">
		<xsl:copy-of select="."/>
	</xsl:template>
</xsl:stylesheet>-->

	<!--<xsl:template match="/">
		<metadata>
			<Esri>
				<Server>
					<xsl:value-of select="//gmd:identificationInfo/srv:SV_ServiceIdentification/srv:containsOperations[1]/srv:SV_OperationMetadata/srv:connectPoint/gmd:CI_OnlineResource/gmd:linkage/gmd:URL"/>
				</Server>
				<Service>
				<xsl:value-of select="//gmd:identificationInfo/srv:SV_ServiceIdentification/srv:containsOperations[1]/srv:SV_OperationMetadata/srv:DCP/srv:DCPList/@codeListValue"/>
				</Service>
				<ServiceType>
						<xsl:value-of select=" //gmd:identificationInfo/srv:SV_ServiceIdentification/srv:serviceType/gco:LocalName/text()"/>
				</ServiceType>
				<ServiceParam>request=<xsl:value-of select="//gmd:identificationInfo/srv:SV_ServiceIdentification/srv:containsOperations[1]/srv:SV_OperationMetadata/srv:operationName/gco:CharacterString/text()"/>
				</ServiceParam>
				<issecured>
					<xsl:value-of select="//Esri/issecured"/>
				</issecured>
				<resourceType>001</resourceType>
			</Esri>
		</metadata>
	</xsl:template>-->
	
	<xsl:template match="/">
		  <xsl:apply-templates select="//gmd:identificationInfo/srv:SV_ServiceIdentification/srv:containsOperations[1]/srv:SV_OperationMetadata/srv:connectPoint/gmd:CI_OnlineResource/gmd:linkage/gmd:URL"/>
	  </xsl:template>
	  
	   <xsl:template match="//gmd:identificationInfo/srv:SV_ServiceIdentification/srv:containsOperations[1]/srv:SV_OperationMetadata/srv:connectPoint/gmd:CI_OnlineResource/gmd:linkage/gmd:URL">
		    <xsl:value-of select="."/>
		      <xsl:text>&#x2714;</xsl:text>
		         urn:x-esri:specification:ServiceType:ArcIMS:Metadata:Server
	     <xsl:text>&#x2715;</xsl:text>
	  </xsl:template>
	  
</xsl:stylesheet>

