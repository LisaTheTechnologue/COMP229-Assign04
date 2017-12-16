<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Landing.aspx.cs" Inherits="COMP229_Assign04._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <!--
--a.	identify the game (Wrath of Kings) and its brand.
--b.	provide a list of all the models in a models collection based on the assign04.json file. 
        These details will be obtained using deserializtion.
--c.	allow for the addition of new models, updating the model collection.
d.	allow for selecting a single model for display on the Model Page.
--e.	allow for saving the updated models collection to a new json file via serialization.
--f.	allow for emailing of the new models json file to an email of the user’s choice. See Instruction 7.
-->
    <div class="jumbotron">
    </div>
    <div class="landing-body">
        <asp:GridView ID="listModel" runat="server" AutoGenerateColumns="false" CssClass="listModelStyle" RowDataBound="listModel_RowDataBound" EnableViewState="false">
            <Columns>
                 <asp:HyperLinkField DataNavigateUrlFields="charName" DataTextField="charName" 
                    DataNavigateUrlFormatString="Model.aspx?Name={0}"  HeaderText="Name" />     
                <asp:BoundField DataField="faction" HeaderText="Faction" />
                <asp:BoundField DataField="rank" HeaderText="Rank" />
                <asp:BoundField DataField="_base" HeaderText="Base" />
                <asp:BoundField DataField="size" HeaderText="Size" />
                <asp:BoundField DataField="deploymentZone" HeaderText="Deployment Zone" />
                             
           </Columns>
            
        </asp:GridView>
        <br />
        <%--<asp:Button ID="showAdd" runat="server" CssClass="btn btn-primary" OnClick="ShowAdd_Click" Text="Add a New Char" />--%>
    </div>

    <div id="addition" runat="server">
        <div class="row">
            <div class="col-md-4">
                <asp:Label ID="lbName" runat="server" Text="Name: " />
            </div>
            <div class="col-md-8">
                <asp:TextBox ID="tbName" runat="server" />
                <asp:RequiredFieldValidator ID="tbNameValidate" runat="server" ErrorMessage="Required" ControlToValidate="tbName"/>
            </div>
            <div class="col-md-4">
                <asp:Label ID="lbFaction" runat="server" Text="Faction: " />
            </div>
            <div class="col-md-8">
                <asp:TextBox ID="tbFaction" runat="server" />
                <asp:RequiredFieldValidator ID="tbFactionValidate" runat="server" ErrorMessage="Required" ControlToValidate="tbFaction"/>
            </div>
            <div class="col-md-4">
                <asp:Label ID="lbRank" runat="server" Text="Rank: " />
            </div>
            <div class="col-md-8">
                <asp:TextBox ID="tbRank" runat="server" />
                <asp:RequiredFieldValidator ID="tbRankValidate" runat="server" ErrorMessage="Required" ControlToValidate="tbRank"/>
            </div>
            <div class="col-md-4">
                <asp:Label ID="lbBase" runat="server" Text="Base: " />
            </div>
            <div class="col-md-8">
                <asp:TextBox ID="tbBase" runat="server" />
                <asp:RequiredFieldValidator ID="tbBaseValidate" runat="server" ErrorMessage="Required" ControlToValidate="tbBase"/>
            </div>
            <div class="col-md-4">
                <asp:Label ID="lbSize" runat="server" Text="Size " />
            </div>
            <div class="col-md-8">
                <asp:TextBox ID="tbSize" runat="server" />
                <asp:RequiredFieldValidator ID="tbSizeValidate" runat="server" ErrorMessage="Required" ControlToValidate="tbSize"/>
            </div>
            <div class="col-md-4">
                <asp:Label ID="lbDZone" runat="server" Text="Deployment Zone" />
            </div>
            <div class="col-md-8">
                <asp:TextBox ID="tbDZone" runat="server" />
                <asp:RequiredFieldValidator ID="tbDZoneValidate" runat="server" ErrorMessage="Required" ControlToValidate="tbDZone"/>
            </div>
            <div class="col-md-4">
                <asp:Label ID="lbActionName" runat="server" Text="Action Name: " />
            </div>
            <div class="col-md-8">
                <asp:TextBox ID="tbActionName" runat="server" />
                <asp:RequiredFieldValidator ID="tbActionNameValidate" runat="server" ErrorMessage="Required" ControlToValidate="tbActionName"/>
            </div>
            <div class="col-md-4">
                <asp:Label ID="lbSpcAbl" runat="server" Text="Special Ability Name:" />
            </div>
            <div class="col-md-8">
                <asp:TextBox ID="tbSpcAbl" runat="server" />
                <asp:RequiredFieldValidator ID="tbSpcAblValidate" runat="server" ErrorMessage="Required" ControlToValidate="tbSpcAbl"/>
            </div>
            <div class="col-md-6">
                <asp:Button ID="addModel" runat="server" CssClass="btn btn-primary" OnClick="addModel_Click" Text="Add" />
            </div>
            <div class="col-md-6">
                <asp:Button ID="cancelForm" runat="server" CssClass="btn btn-primary" OnClick="cancelModel_Click" Text="Cancel" />
            </div>
        </div>
        <div id="errorMsg" runat="server"></div>
    </div>
</asp:Content>
