<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:rim="urn:oasis:names:tc:ebxml-regrep:rim:xsd:2.5" xmlns:gml="http://www.opengis.net/gml" xmlns:csw="http://www.opengis.net/cat/csw" xmlns:dct="http://purl.org/dc/terms/" xmlns:dc="http://purl.org/dc/elements/1.1/">
	<xsl:output method="xml" indent="yes" encoding="UTF-8" omit-xml-declaration="no"/>
	<xsl:template match="/">
		<Records>
			<xsl:for-each select="/csw:GetRecordsResponse/csw:SearchResults/rim:ExtrinsicObject">
				<Record>
					<ID>
						<xsl:value-of select="@id"/>
					</ID>
					<Title>
						<xsl:value-of select="rim:Slot[@name='http://purl.org/dc/elements/1.1/title']/rim:ValueList/rim:Value"/>
					</Title>
					<Abstract>
						<xsl:value-of select="rim:Description/rim:LocalizedString/@value"/>
					</Abstract>
					<Type>liveData</Type>
					<xsl:choose>
						<xsl:when test="count(rim:Slot[@name='Polygon']/rim:ValueList/rim:Value)>0">
					<xsl:variable name="x1" select="substring-before(substring-before(rim:Slot[@name='Polygon']/rim:ValueList/rim:Value,' '),',')"/>
					<xsl:variable name="x2" select="substring-before(substring-before(substring-after(rim:Slot[@name='Polygon']/rim:ValueList/rim:Value,' '),' '),',')"/>
					<xsl:variable name="y1" select="substring-after(substring-before(rim:Slot[@name='Polygon']/rim:ValueList/rim:Value,' '),',')"/>
					<xsl:variable name="y2" select="substring-before(substring-after(substring-after(substring-after(rim:Slot[@name='Polygon']/rim:ValueList/rim:Value,' '),' '),','),' ')"/>
					<xsl:choose>
						<xsl:when test="$x1 &lt; $x2">
							<MinX>
								<xsl:value-of select="$x1"/>
							</MinX>
							<MaxX>
								<xsl:value-of select="$x2"/>
							</MaxX>
						</xsl:when>
						<xsl:otherwise>
							<MinX>
								<xsl:value-of select="$x2"/>
							</MinX>
							<MaxX>
								<xsl:value-of select="$x1"/>
							</MaxX>
						</xsl:otherwise>
					</xsl:choose>
					<xsl:choose>
						<xsl:when test="$y1 &lt; $y2">
							<MinY>
								<xsl:value-of select="$y1"/>
							</MinY>
							<MaxY>
								<xsl:value-of select="$y2"/>
							</MaxY>
						</xsl:when>
						<xsl:otherwise>
							<MinY>
								<xsl:value-of select="$y2"/>
							</MinY>
							<MaxY>
								<xsl:value-of select="$y1"/>
							</MaxY>
						</xsl:otherwise>
					</xsl:choose>
						</xsl:when>
						<xsl:otherwise>
							<MinX>-180</MinX>
							<MaxX>180</MaxX>
							<MinY>-90</MinY>
							<MaxY>90</MaxY>
						</xsl:otherwise>
					</xsl:choose>
				</Record>
			</xsl:for-each>
		</Records>
	</xsl:template>
</xsl:stylesheet>
