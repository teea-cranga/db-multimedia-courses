<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdaugaVideo.aspx.cs" Inherits="Seminar1_BDMultimedia.AdaugaVideo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form2" runat="server">
        <h1>
            <asp:Label ID="addVideoLbl" runat="server" BackColor="#993300" Text="BugID - Adauga video"></asp:Label>
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Postare noua" />
            <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Vezi toate postarile" />
        </h1>
        <p>
            <asp:Label runat="server" Text="ID imagine: " ID="id_video"></asp:Label><asp:TextBox runat="server" ID="id_vid"></asp:TextBox>
        </p>
        <p>
            <asp:Label ID="Label1" runat="server" Text="Video: "></asp:Label>
            &nbsp;<asp:FileUpload ID="fileupload" runat="server" />
        </p>
        <p>
            <asp:Button runat="server" Text="Adauga video" ID="addVideoBtn" style="margin-bottom: 0px" Height="26px" Width="115px" OnClick="ButtonUpload_Click"></asp:Button>
            <asp:Button ID="Button3" runat="server" Text="Afiseaza video" OnClick="Button3_Click" />
            <video id="video" runat="server" type="video/mp4" autoplay="autoplay" controls="controls"></video>
        </p>

    </form>
</body>
</html>
