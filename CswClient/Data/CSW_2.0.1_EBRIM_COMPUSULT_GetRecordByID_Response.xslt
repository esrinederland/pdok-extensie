<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:rim="urn:oasis:names:tc:ebxml-regrep:rim:xsd:2.5" xmlns:gml="http://www.opengis.net/gml" xmlns:csw="http://www.opengis.net/cat/csw" xmlns:dct="http://purl.org/dc/terms/" xmlns:dc="http://purl.org/dc/elements/1.1/">
	<xsl:output method="xml" indent="yes" encoding="UTF-8" omit-xml-declaration="no"/>
	<xsl:template match="/">
		<rdf:RDF xmlns:rdf="http://www.w3.org/1999/02/22-rdf-syntax-ns#" xmlns:dc="http://purl.org/dc/elements/1.1/" xmlns:ows="http://www.opengis.net/ows" xmlns:dct="http://purl.org/dc/terms/" xmlns:dcmiBox="http://dublincore.org/documents/2000/07/11/dcmi-box/">
			<xsl:for-each select="/csw:GetRecordByIdResponse/rim:CSWExtrinsicObject">
				<rdf:Description about="http://dublincore.org/">
					<dc:identifier>
						<xsl:value-of select="@id"/>
					</dc:identifier>
					<dc:title xml:lang="en">
						<xsl:value-of select="rim:Slot[@name='http://purl.org/dc/elements/1.1/title']/rim:ValueList/rim:Value"/>
					</dc:title>
					<dc:description>
						<xsl:value-of select="rim:Description/rim:LocalizedString/@value"/>
					</dc:description>
					<dc:date><xsl:value-of select="substring-before(rim:Slot[@name='Harvest Date']/rim:ValueList/rim:Value,' ')"/></dc:date>
					<dc:format>text/xml</dc:format>
					<dc:language>eng</dc:language>
					<dc:contributor></dc:contributor>
					<dc:type>liveData</dc:type>
					<dc:creator><xsl:value-of select="rim:Slot[@name='Originator']/rim:ValueList/rim:Value"/></dc:creator>
					<dc:subject>farming</dc:subject>
					<dc:subject>biota</dc:subject>
					<dc:subject>boundaries</dc:subject>
					<dc:subject>climatologyMeteorologyAtmosphere</dc:subject>
					<dc:subject>economy</dc:subject>
					<dc:subject>elevation</dc:subject>
					<dc:subject>environment</dc:subject>
					<dc:subject>geoscientificInformation</dc:subject>
					<dc:subject>health</dc:subject>
					<dc:subject>imageryBaseMapsEarthCover</dc:subject>
					<dc:subject>intelligenceMilitary</dc:subject>
					<dc:subject>inlandWaters</dc:subject>
					<dc:subject>location</dc:subject>
					<dc:subject>oceans</dc:subject>
					<dc:subject>planningCadastre</dc:subject>
					<dc:subject>society</dc:subject>
					<dc:subject>structure</dc:subject>
					<dc:subject>transportation</dc:subject>
					<dc:subject>utilitiesCommunication</dc:subject>
					<dct:references/>
					<ows:WGS84BoundingBox>
						<westbc>-180</westbc>
						<eastbc>180</eastbc>
						<southbc>-90</southbc>
						<northbc>90</northbc>
					</ows:WGS84BoundingBox>
				</rdf:Description>
			</xsl:for-each>
		</rdf:RDF>
	</xsl:template>
</xsl:stylesheet>
