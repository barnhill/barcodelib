<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<link rel="Stylesheet" type="text/css" href="stylesheet.css" />
    <title>Barcode Page</title>
    <style type="text/css">
        .style1
        {
            width: 129px;
        }
        .style3
        {
            text-align: left;
            color: #0066FF;
            font-weight: bold;
            font-size: x-large;
        }
        </style>
    <script type="text/javascript" language="javascript">
    function colorChanged(sender) 
    {
        sender.get_element().style.backcolor = 
        "#" + sender.get_selectedColor();
    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="style3">
            Barcode Demo
            <asp:Label ID="lblLibraryVersion" runat="server" Text="[Library Version]"></asp:Label>
        </div>
        <table bgcolor="White">
            <tr>
                <td style="background-color: #FFFFCC; border: 1px solid #000000">
                    <asp:UpdatePanel ID="panelInputs" runat="server">
                        <ContentTemplate>
                            <table>
                                <tr>
                                    <td class="style1">
                                        <span lang="en-us">Value to Encode</span></td>
                                    <td>
                                        <span lang="en-us">
                                        <asp:CheckBox ID="chkGenerateLabel" runat="server" Text="Generate Label" />
                                        </span>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:TextBox ID="txtData" runat="server" Width="264px">038000356216</asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style1" align="center">
                                        <span lang="en-us">Encoding</span></td>
                                    <td align="center">
                                        <span lang="en-us">Image Format</span></td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:DropDownList ID="cbEncodeType" runat="server">
                                            <asp:ListItem>UPC-A</asp:ListItem>
                                            <asp:ListItem>UPC-E</asp:ListItem>
                                            <asp:ListItem>UPC 2 Digit Ext</asp:ListItem>
                                            <asp:ListItem>UPC 5 Digit Ext</asp:ListItem>
                                            <asp:ListItem>EAN-13</asp:ListItem>
                                            <asp:ListItem>JAN-13</asp:ListItem>
                                            <asp:ListItem>EAN-8</asp:ListItem>
                                            <asp:ListItem>ITF-14</asp:ListItem>
                                            <asp:ListItem>Interleaved 2 of 5</asp:ListItem>
                                            <asp:ListItem>Standard 2 of 5</asp:ListItem>
                                            <asp:ListItem>Codabar</asp:ListItem>
                                            <asp:ListItem>Postnet</asp:ListItem>
                                            <asp:ListItem>Bookland-ISBN</asp:ListItem>
                                            <asp:ListItem>Code 11</asp:ListItem>
                                            <asp:ListItem>Code 39</asp:ListItem>
                                            <asp:ListItem>Code 39 Extended</asp:ListItem>
                                            <asp:ListItem>Code 93</asp:ListItem>
                                            <asp:ListItem>Code 128</asp:ListItem>
                                            <asp:ListItem>Code 128-A</asp:ListItem>
                                            <asp:ListItem>Code 128-B</asp:ListItem>
                                            <asp:ListItem>Code 128-C</asp:ListItem>
                                            <asp:ListItem>LOGMARS</asp:ListItem>
                                            <asp:ListItem>MSI</asp:ListItem>
                                            <asp:ListItem>Telepen</asp:ListItem>
                                            <asp:ListItem>FIM (Facing Identification Mark)</asp:ListItem>
                                            <asp:ListItem>Pharmacode</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td align="center">
                                        <asp:DropDownList ID="ddlImageFormat" runat="server">
                                            <asp:ListItem>GIF</asp:ListItem>
                                            <asp:ListItem>JPEG</asp:ListItem>
                                            <asp:ListItem>PNG</asp:ListItem>
                                            <asp:ListItem>TIFF</asp:ListItem>
                                            <asp:ListItem>BMP</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style1">
                                        <asp:Button ID="btnEncode" runat="server" Text="Encode" Width="100px" 
                                            onclick="btnEncode_Click" UseSubmitBehavior="False" />
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Label ID="Label1" runat="server" Text="Alignment:"></asp:Label>
                                        &nbsp;
                                        <asp:RadioButton ID="rbAlignLeft" runat="server" GroupName="alignment" 
                                            Text="Left" />
                                        &nbsp;&nbsp;
                                        <asp:RadioButton ID="rbAlignCenter" runat="server" Checked="True" 
                                            GroupName="alignment" Text="Center" />
                                        &nbsp;&nbsp;
                                        <asp:RadioButton ID="rbAlignRight" runat="server" GroupName="alignment" 
                                            Text="Right" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <span lang="en-us">Foreground Color</span></td>
                                    <td>
                                        <span lang="en-us">Background Color</span></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtForeColor" runat="server" 
                        ReadOnly="False" BackColor="Black"></asp:TextBox>
                                        <cc1:ColorPickerExtender ID="ColorPickerExtender1" runat="server" 
                        TargetControlID="txtForeColor" OnClientColorSelectionChanged="colorChanged"/>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtBackColor" runat="server" 
                        ReadOnly="False"></asp:TextBox>
                                        <cc1:ColorPickerExtender ID="ColorPickerExtender2" runat="server" 
                        TargetControlID="txtBackColor" OnClientColorSelectionChanged="colorChanged"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <span lang="en-us">Width</span></td>
                                    <td>
                                        <span lang="en-us">Height</span></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtWidth" runat="server">300</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtHeight" runat="server">150</asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td style="border-color: #000000; border-width: 1px; border-top-style: solid; border-bottom-style: solid">
                    &nbsp;</td>
                <td style="border-color: #000000; border-width: 1px; border-top-style: solid; border-bottom-style: solid">
                    <asp:UpdatePanel ID="panelBarcode" runat="server">
                        <ContentTemplate>
                            <asp:Image ID="BarcodeImage" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td style="border-color: #000000; border-width: 1px; border-top-style: solid; border-bottom-style: solid; border-right-style: solid;">
                    &nbsp;</td>
            </tr>
            </table>
        <br />
    
    </div>
    </form>
</body>
</html>
