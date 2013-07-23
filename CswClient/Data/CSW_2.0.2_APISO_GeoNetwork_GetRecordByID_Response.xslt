<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:csw="http://www.opengis.net/cat/csw/2.0.2" xmlns:gco="http://www.isotc211.org/2005/gco" xmlns:gmd="http://www.isotc211.org/2005/gmd" xmlns:srv="http://www.isotc211.org/2005/srv" exclude-result-prefixes="">
	<xsl:output method="html" indent="yes" encoding="UTF-8"/>
	<xsl:template match="/csw:GetRecordByIdResponse/gmd:MD_Metadata">
<!--		<metadata>
			<Esri>
				<xsl:choose>
					<xsl:when test="count(/csw:GetRecordByIdResponse/gmd:MD_Metadata/gmd:distributionInfo/gmd:MD_Distribution/gmd:transferOptions/gmd:MD_DigitalTransferOptions/gmd:onLine/gmd:CI_OnlineResource[gmd:protocol/gco:CharacterString='OGC:WMS'])>0">
						<Server>
							<xsl:value-of select="/csw:GetRecordByIdResponse/gmd:MD_Metadata/gmd:distributionInfo/gmd:MD_Distribution/gmd:transferOptions/gmd:MD_DigitalTransferOptions/gmd:onLine/gmd:CI_OnlineResource[gmd:protocol/gco:CharacterString='OGC:WMS']/gmd:linkage/gmd:URL"/>
						</Server>
						<Service>
							<xsl:value-of select="//gmd:identificationInfo/srv:SV_ServiceIdentification/srv:containsOperations[1]/srv:SV_OperationMetadata/srv:DCP/srv:DCPList/@codeListValue"/>
						</Service>
						<ServiceType>wms</ServiceType>
						<ServiceParam>request=GetCapabilities&amp;service=WMS&amp;version=<xsl:value-of select="/csw:GetRecordByIdResponse/gmd:MD_Metadata/gmd:distributionInfo/gmd:MD_Distribution/gmd:distributionFormat/gmd:MD_Format[gmd:name/gco:CharacterString='wms']/gmd:version/gco:CharacterString"/></ServiceParam>
						<issecured>false</issecured>
						<resourceType>001</resourceType>
					</xsl:when>
					<xsl:otherwise>
						<Server></Server>
						<Service></Service>
						<ServiceType></ServiceType>
						<ServiceParam></ServiceParam>
						<issecured>
							<xsl:value-of select="//Esri/issecured"/>
						</issecured>
						<resourceType>001</resourceType>						
					</xsl:otherwise>
				</xsl:choose>
			</Esri>
		</metadata>
-->
	</xsl:template>
</xsl:stylesheet>
