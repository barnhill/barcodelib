using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.BarcodeImage.Visible = false;

        lblLibraryVersion.Text = BarcodeLib.Barcode.Version.ToString();
    }

    protected void btnEncode_Click(object sender, EventArgs e)
    {
        string strImageURL = "GenerateBarcodeImage.aspx?d=" + this.txtData.Text.Trim() + "&h=" + this.txtHeight.Text.Trim() + "&w=" + this.txtWidth.Text.Trim() + "&bc=" + this.txtBackColor.Text.Trim() + "&fc=" + this.txtForeColor.Text.Trim() + "&t=" + this.cbEncodeType.SelectedValue + "&il=" + this.chkGenerateLabel.Checked.ToString() + "&if=" + this.ddlImageFormat.SelectedValue + "&align=" + GetAlignmentValue();
        this.BarcodeImage.ImageUrl = strImageURL;
        this.BarcodeImage.Width = Convert.ToInt32(this.txtWidth.Text);
        this.BarcodeImage.Height = Convert.ToInt32(this.txtHeight.Text);
        this.BarcodeImage.Visible = true;
    }

    protected string GetAlignmentValue()
    {
        if (this.rbAlignCenter.Checked)
            return "c";
        else if (this.rbAlignLeft.Checked)
            return "l";
        else if (this.rbAlignRight.Checked)
            return "r";
        return "c";
    }
}
