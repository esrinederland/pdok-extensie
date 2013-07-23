<?xml version="1.0" encoding="UTF-8"?>

<!-- XML Namespaces -->
<xsl:stylesheet version="2.0"
xmlns:xsl="http://www.w3.org/1999/XSL/Transform" 
xmlns:dc="http://purl.org/dc/elements/1.1/"
xmlns:dct="http://purl.org/dc/terms/"
xmlns:csw="http://www.opengis.net/cat/csw/2.0.2"
xmlns:gmd="http://www.isotc211.org/2005/gmd" 
xmlns:gmx="http://www.isotc211.org/2005/gmx" 
xmlns:srv="http://www.isotc211.org/2005/srv" 
xmlns:gml="http://www.opengis.net/gml" 
xmlns:xs="http://www.w3.org/2001/XMLSchema" 
xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" 
xmlns:gts="http://www.isotc211.org/2005/gts" 
xmlns:gco="http://www.isotc211.org/2005/gco" 
xmlns:gsr="http://www.isotc211.org/2005/gsr" 
xmlns:xlink="http://www.w3.org/1999/xlink" 
xmlns:geonet="http://www.fao.org/geonetwork" 
xsi:schemaLocation="http://www.isotc211.org/2005/gmd http://schemas.opengis.net/iso/19139/20060504/gmd/gmd.xsd http://www.isotc211.org/2005/srv http://schemas.opengis.net/iso/19139/20060504/srv/srv.xsd"
exclude-result-prefixes="csw dc dct gmd gco srv ">

<!-- Output Parameters -->
<xsl:output method="html" escape-uri-attributes="yes" indent="yes"/>

<!-- Define where to start  styling the XML -->
<xsl:template match="csw:GetRecordByIdResponse/gmd:MD_Metadata">

<!-- Start HTML output -->	
	<html>
		<body>
		
<!-- Title of the metadata item -->		
			<h2>Metadata van het geselecteerde item</h2>
			
<!-- Show Metadata in table structure -->			
			<table border="1" width="500">
			
<!-- Table Header -->			
				<tr bgcolor="cfcfcf">
					<th width="150"><h3>Kenmerk</h3></th>
					<th width="350"><h3>Metadata</h3></th>
				</tr>

<!-- Table Body -->
<!-- There can be more than one items for certain elements, hence xml:for-each -->	
				
				<!-- Identification Info -->
				<tr><td colspan="2"><b><br /><em>Identificatie informatie</em></b></td></tr>
				<xsl:for-each select="gmd:identificationInfo/srv:SV_ServiceIdentification/gmd:citation/gmd:CI_Citation/gmd:title"> 
					<tr>
						<td><b>Titel</b></td>
						<td><xsl:value-of select="current()"/></td>
					</tr>      
				</xsl:for-each> 
				<xsl:for-each select="gmd:identificationInfo/srv:SV_ServiceIdentification/gmd:citation/gmd:CI_Citation/gmd:date/gmd:CI_Date/gmd:date"> 
					<tr>
						<td><b>Datum</b></td>
						<td><xsl:value-of select="current()"/></td>
					</tr>      
				</xsl:for-each> 
				<xsl:for-each select="gmd:identificationInfo/srv:SV_ServiceIdentification/gmd:citation/gmd:CI_Citation/gmd:date/gmd:CI_Date/gmd:dateType"> 
					<tr>
						<td><b>Datum Type</b></td>
						<td><xsl:value-of select="current()"/></td>
					</tr>      
				</xsl:for-each> 				
				<xsl:for-each select="gmd:identificationInfo/srv:SV_ServiceIdentification/gmd:abstract"> 
					<tr>
						<td><b>Abstract</b></td>
						<td><xsl:value-of select="current()"/></td>
					</tr>      
				</xsl:for-each> 	
				
				<!-- Contact -->
				<tr><td colspan="2"><br /><b><em>Verantwoordelijke partij</em></b></td></tr>
				<xsl:for-each select="gmd:contact/gmd:CI_ResponsibleParty/gmd:individualName"> 
					<tr>
						<td><b>Naam van de persoon</b></td>
						<td><xsl:value-of select="current()"/></td>
					</tr>      
				</xsl:for-each> 				
				<xsl:for-each select="gmd:contact/gmd:CI_ResponsibleParty/gmd:organisationName"> 
					<tr>
						<td><b>Naam van de organisatie</b></td>
						<td><xsl:value-of select="current()"/></td>
					</tr>      
				</xsl:for-each> 				
				<xsl:for-each select="gmd:contact/gmd:CI_ResponsibleParty/gmd:contactInfo/gmd:CI_Contact/gmd:address/gmd:CI_Address/gmd:electronicMailAddress"> 
					<tr>
						<td><b>E-mail adres</b></td>
						<td><xsl:value-of select="current()"/></td>
					</tr>      
				</xsl:for-each> 				

				<!-- Date Stamp -->
				<tr><td colspan="2"><br /><b><em>Datum stempel</em></b></td></tr>				
				<xsl:for-each select="gmd:dateStamp"> 
					<tr>
						<td><b>Datum</b></td>
						<td><xsl:value-of select="current()"/></td>
					</tr>      
				</xsl:for-each>
				
				<!-- Metadata Standard Name -->	
				<tr><td colspan="2"><br /><b><em>Metadata Standaard</em></b></td></tr>				
				<xsl:for-each select="gmd:metadataStandardName"> 
					<tr>
						<td><b>Naam</b></td>
						<td><xsl:value-of select="current()"/></td>
					</tr>      
				</xsl:for-each>
				

				<!-- Point of Contact -->	
				<tr><td colspan="2"><br /><b><em>Verantwoordelijke partij</em></b></td></tr>				
				<xsl:for-each select="gmd:identificationInfo/srv:SV_ServiceIdentification/gmd:pointOfContact/gmd:CI_ResponsibleParty/gmd:organisationName"> 
					<tr>
						<td><b>Organisatie</b></td>
						<td><xsl:value-of select="current()"/></td>
					</tr>      
				</xsl:for-each>
				<xsl:for-each select="gmd:identificationInfo/srv:SV_ServiceIdentification/gmd:pointOfContact/gmd:CI_ResponsibleParty/gmd:contactInfo/gmd:CI_Contact/gmd:address/gmd:CI_Address/gmd:electronicMailAddress"> 
					<tr>
						<td><b>E-mail adres</b></td>
						<td><xsl:value-of select="current()"/></td>
					</tr>      
				</xsl:for-each>

<!--
				Graphic Overview
				<tr><td colspan="2"><br /><b><em>Bestandsnaam</em></b></td></tr>				
				<xsl:for-each select="gmd:identificationInfo/srv:SV_ServiceIdentification/gmd:graphicOverview/gmd:MD_BrowseGraphic/gmd:fileName"> 
					<tr>
						<td><b>Bestandsnaam</b></td>
						<td><xsl:value-of select="current()"/></td>
					</tr>      
				</xsl:for-each>
-->				


				<!--  Descriptive keywords -->
				<tr><td colspan="2"><br /><b><em>Beschrijvende sleutelwoorden</em></b></td></tr>				
				<xsl:for-each select="gmd:identificationInfo/srv:SV_ServiceIdentification/gmd:descriptiveKeywords/gmd:MD_Keywords/gmd:keyword"> 
					<tr>
						<td><b>Sleutelwoord</b></td>
						<td><xsl:value-of select="current()"/></td>
					</tr>      
				</xsl:for-each>
				
				
<!--
				<xsl:for-each select="gmd:identificationInfo/srv:SV_ServiceIdentification/gmd:descriptiveKeywords/gmd:MD_Keywords/gmd:thesaurusName/gmd:CI_Citation/gmd:title"> 
					<tr>
						<td><b>Titel</b></td>
						<td><xsl:value-of select="current()"/></td>
					</tr>      
				</xsl:for-each>
				<xsl:for-each select="gmd:identificationInfo/srv:SV_ServiceIdentification/gmd:descriptiveKeywords/gmd:MD_Keywords/gmd:thesaurusName/gmd:CI_Citation/gmd:date"> 
					<tr>
						<td><b>Titel</b></td>
						<td><xsl:value-of select="current()"/></td>
					</tr>      
				</xsl:for-each>

-->				


				<!--  Gebruiksbeperkingen -->
				<tr><td colspan="2"><br /><b><em>Gebruiksbeperkingen</em></b></td></tr>				
				<xsl:for-each select="gmd:identificationInfo/srv:SV_ServiceIdentification/gmd:resourceConstraints/gmd:MD_Constraints/gmd:useLimitation"> 
					<tr>
						<td><b>Gebruiksbeperkingen</b></td>
						<td><xsl:value-of select="current()"/></td>
					</tr>      
				</xsl:for-each>

				<!--  Security Constraints -->
				<tr><td colspan="2"><br /><b><em>Beveiligingsbeperkingen</em></b></td></tr>				
				<xsl:for-each select="gmd:identificationInfo/srv:SV_ServiceIdentification/gmd:resourceConstraints/gmd:MD_SecurityConstraints/gmd:classification"> 
					<tr>
						<td><b>Beveiligingsbeperkingen</b></td>
						<td><xsl:value-of select="current()"/></td>
					</tr>      
				</xsl:for-each>

				<!--  Legal Constraints -->
				<tr><td colspan="2"><br /><b><em>Gerechtelijke beperkingen</em></b></td></tr>				
				<xsl:for-each select="gmd:identificationInfo/srv:SV_ServiceIdentification/gmd:resourceConstraints/gmd:MD_LegalConstraints/gmd:accessConstraints"> 
					<tr>
						<td><b>Toegangsbeperkingen</b></td>
						<td><xsl:value-of select="current()"/></td>
					</tr>      
				</xsl:for-each>
				<xsl:for-each select="gmd:identificationInfo/srv:SV_ServiceIdentification/gmd:resourceConstraints/gmd:MD_LegalConstraints/gmd:otherConstraints"> 
					<tr>
						<td><b>Overige Beperkingen</b></td>
						<td><xsl:value-of select="current()"/></td>
					</tr>      
				</xsl:for-each>

				<!--  Service Type -->
				<tr><td colspan="2"><br /><b><em>Service type</em></b></td></tr>				
				<xsl:for-each select="gmd:identificationInfo/srv:SV_ServiceIdentification/gmd:serviceType"> 
					<tr>
						<td><b>Service Type</b></td>
						<td><xsl:value-of select="current()"/></td>
					</tr>      
				</xsl:for-each>

				<!--  Extent -->
				<tr><td colspan="2"><br /><b><em>Extent</em></b></td></tr>				
				<xsl:for-each select="gmd:identificationInfo/srv:SV_ServiceIdentification/srv:extent/gmd:EX_Extent/gmd:geographicElement/gmd:EX_GeographicBoundingBox/gmd:westBoundLongitude"> 
					<tr>
						<td><b>Westerlijke lengtegraad</b></td>
						<td><xsl:value-of select="current()"/></td>
					</tr>      
				</xsl:for-each>
				<xsl:for-each select="gmd:identificationInfo/srv:SV_ServiceIdentification/srv:extent/gmd:EX_Extent/gmd:geographicElement/gmd:EX_GeographicBoundingBox/gmd:eastBoundLongitude"> 
					<tr>
						<td><b>Oosterlijke lengtegraad</b></td>
						<td><xsl:value-of select="current()"/></td>
					</tr>      
				</xsl:for-each>
				<xsl:for-each select="gmd:identificationInfo/srv:SV_ServiceIdentification/srv:extent/gmd:EX_Extent/gmd:geographicElement/gmd:EX_GeographicBoundingBox/gmd:southBoundLatitude"> 
					<tr>
						<td><b>Zuiderlijke breedtegraad</b></td>
						<td><xsl:value-of select="current()"/></td>
					</tr>      
				</xsl:for-each>
				<xsl:for-each select="gmd:identificationInfo/srv:SV_ServiceIdentification/srv:extent/gmd:EX_Extent/gmd:geographicElement/gmd:EX_GeographicBoundingBox/gmd:northBoundLatitude"> 
					<tr>
						<td><b>Noorderlijke breedtegraad</b></td>
						<td><xsl:value-of select="current()"/></td>
					</tr>      
				</xsl:for-each>
				<xsl:for-each select="gmd:identificationInfo/srv:SV_ServiceIdentification/srv:extent/gmd:EX_Extent/gmd:temporalElement/gmd:EX_TemporalExtent/gmd:extent/gml:TimePeriod/gml:begin/gml:TimeInstant/gml:timePosition"> 
					<tr>
						<td><b>Begin tijd</b></td>
						<td><xsl:value-of select="current()"/></td>
					</tr>      
				</xsl:for-each>
				<xsl:for-each select="gmd:identificationInfo/srv:SV_ServiceIdentification/srv:extent/gmd:EX_Extent/gmd:temporalElement/gmd:EX_TemporalExtent/gmd:extent/gml:TimePeriod/gml:end/gml:TimeInstant/gml:timePosition"> 
					<tr>
						<td><b>Eind tijd</b></td>
						<td><xsl:value-of select="current()"/></td>
					</tr>      
				</xsl:for-each>				
				
				
				
				<!--  Operation Name -->
				<tr><td colspan="2"><br /><b><em>Metadata opdracht</em></b></td></tr>				
				<xsl:for-each select="srv:containsOperations/srv:SV_OperationMetadata/srv:operationName"> 
					<tr>
						<td><b>Naam van de opdracht</b></td>
						<td><xsl:value-of select="current()"/></td>
					</tr>      
				</xsl:for-each>


				<!--  Online Resource -->
				<tr><td colspan="2"><br /><b><em>Online bron</em></b></td></tr>				
				<xsl:for-each select="srv:containsOperations/srv:SV_OperationMetadata/srv:connectPoint/gmd:CI_OnlineResource/gmd:linkage"> 
					<tr>
						<td><b>URL</b></td>
						<td><xsl:value-of select="current()"/></td>
					</tr>      
				</xsl:for-each>
				<xsl:for-each select="srv:containsOperations/srv:SV_OperationMetadata/srv:connectPoint/gmd:CI_OnlineResource/gmd:protocol"> 
					<tr>
						<td><b>Protocol</b></td>
						<td><xsl:value-of select="current()"/></td>
					</tr>      
				</xsl:for-each>
				<xsl:for-each select="srv:containsOperations/srv:SV_OperationMetadata/srv:connectPoint/gmd:CI_OnlineResource/gmd:description"> 
					<tr>
						<td><b>Beschrijving</b></td>
						<td><xsl:value-of select="current()"/></td>
					</tr>      
				</xsl:for-each>				

				
<!--
				  Distributor 
				<tr><td colspan="2"><br /><b><em>Verantwoordelijke Leverancier</em></b></td></tr>				
				<xsl:for-each select="gmd:distributionInfo/gmd:MD_Distribution/gmd:distributor/gmd:MD_Distributor/gmd:distributorContact/gmd:CI_ResponsibleParty/gmd:individualName"> 
					<tr>
						<td><b>Naam van de persoon</b></td>
						<td><xsl:value-of select="current()"/></td>
					</tr>      
				</xsl:for-each>
				<xsl:for-each select="gmd:distributionInfo/gmd:MD_Distribution/gmd:distributor/gmd:MD_Distributor/gmd:distributorContact/gmd:CI_ResponsibleParty/gmd:organisationName"> 
					<tr>
						<td><b>Naam van de organisatie</b></td>
						<td><xsl:value-of select="current()"/></td>
					</tr>      
				</xsl:for-each>
				<xsl:for-each select="gmd:distributionInfo/gmd:MD_Distribution/gmd:distributor/gmd:MD_Distributor/gmd:distributorContact/gmd:CI_ResponsibleParty/gmd:positionName"> 
					<tr>
						<td><b>Functie</b></td>
						<td><xsl:value-of select="current()"/></td>
					</tr>      
				</xsl:for-each>				
				<xsl:for-each select="gmd:distributionInfo/gmd:MD_Distribution/gmd:distributor/gmd:MD_Distributor/gmd:distributorContact/gmd:CI_ResponsibleParty/gmd:contactInfo"> 
					<tr>
						<td><b>Contact informatie</b></td>
						<td><xsl:value-of select="current()"/></td>
					</tr>      
				</xsl:for-each>
				<xsl:for-each select="gmd:distributionInfo/gmd:MD_Distribution/gmd:distributor/gmd:MD_Distributor/gmd:distributorContact/gmd:CI_ResponsibleParty/gmd:role"> 
					<tr>
						<td><b>Rol</b></td>
						<td><xsl:value-of select="current()"/></td>
					</tr>      
				</xsl:for-each>
-->				
				
				
				<!--  Overdrachts Opties -->		
				<tr><td colspan="2"><br /><b><em>Overdrachts opties</em></b></td></tr>
				<xsl:for-each select="gmd:distributionInfo/gmd:MD_Distribution/gmd:transferOptions/gmd:MD_DigitalTransferOptions/gmd:onLine/gmd:CI_OnlineResource/gmd:linkage"> 
					<tr>
						<td><b>Link</b></td>
						<td><xsl:value-of select="current()"/></td>
					</tr>      
				</xsl:for-each>
				<xsl:for-each select="gmd:distributionInfo/gmd:MD_Distribution/gmd:transferOptions/gmd:MD_DigitalTransferOptions/gmd:onLine/gmd:CI_OnlineResource/gmd:protocol"> 
					<tr>
						<td><b>Protocol</b></td>
						<td><xsl:value-of select="current()"/></td>
					</tr>      
				</xsl:for-each>
				<xsl:for-each select="gmd:distributionInfo/gmd:MD_Distribution/gmd:transferOptions/gmd:MD_DigitalTransferOptions/gmd:onLine/gmd:CI_OnlineResource/gmd:name"> 
					<tr>
						<td><b>Naam</b></td>
						<td><xsl:value-of select="current()"/></td>
					</tr>      
				</xsl:for-each>				
				<xsl:for-each select="gmd:distributionInfo/gmd:MD_Distribution/gmd:transferOptions/gmd:MD_DigitalTransferOptions/gmd:onLine/gmd:CI_OnlineResource/gmd:description"> 
					<tr>
						<td><b>Beschrijving</b></td>
						<td><xsl:value-of select="current()"/></td>
					</tr>      
				</xsl:for-each>				


				</table>
			
<!-- Footer of the metadata -->			
			<p><em>PDOK extensie voor ArcGIS</em></p>
		</body>
	</html> 
</xsl:template>
</xsl:stylesheet>