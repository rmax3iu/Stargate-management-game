namespace SAE24_Stargate
{
    partial class ucVueStatistiques
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tcStats = new System.Windows.Forms.TabControl();
            this.pageEquipe = new System.Windows.Forms.TabPage();
            this.dgvEquipe = new System.Windows.Forms.DataGridView();
            this.Nom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Prénom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlCbo = new System.Windows.Forms.Panel();
            this.lblEquipe = new System.Windows.Forms.Label();
            this.cboNoms = new System.Windows.Forms.ComboBox();
            this.pageFinances = new System.Windows.Forms.TabPage();
            this.dgvMissions = new System.Windows.Forms.DataGridView();
            this.date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descriprtion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.montant = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.categorie = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlCartes = new System.Windows.Forms.Panel();
            this.tlpMissions = new System.Windows.Forms.TableLayoutPanel();
            this.pnlBudgetA = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lblSommeA = new System.Windows.Forms.Label();
            this.lblBudgetA = new System.Windows.Forms.Label();
            this.pnlC1 = new System.Windows.Forms.Panel();
            this.grpMissions = new System.Windows.Forms.GroupBox();
            this.cboMissions = new System.Windows.Forms.ComboBox();
            this.pnlBudget = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblSomme = new System.Windows.Forms.Label();
            this.lblBudgetI = new System.Windows.Forms.Label();
            this.pageExplorations = new System.Windows.Forms.TabPage();
            this.ccPlanetes = new System.Windows.Forms.Integration.ElementHost();
            this.cartesianChart1 = new LiveCharts.Wpf.CartesianChart();
            this.lblPlanetes = new System.Windows.Forms.Label();
            this.lblGraph = new System.Windows.Forms.Label();
            this.pageDepenses = new System.Windows.Forms.TabPage();
            this.dgvDepenses = new System.Windows.Forms.DataGridView();
            this.depenses = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nomMission = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nomChef = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlDepenses = new System.Windows.Forms.Panel();
            this.lblTitre = new System.Windows.Forms.Label();
            this.tabInformateurs = new System.Windows.Forms.TabPage();
            this.dgvInformateurs = new System.Windows.Forms.DataGridView();
            this.nomCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nomEspece = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.somme = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblInformateurs = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboInformateurs = new System.Windows.Forms.ComboBox();
            this.tcStats.SuspendLayout();
            this.pageEquipe.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEquipe)).BeginInit();
            this.pnlCbo.SuspendLayout();
            this.pageFinances.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMissions)).BeginInit();
            this.pnlCartes.SuspendLayout();
            this.tlpMissions.SuspendLayout();
            this.pnlBudgetA.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.pnlC1.SuspendLayout();
            this.grpMissions.SuspendLayout();
            this.pnlBudget.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.pageExplorations.SuspendLayout();
            this.pageDepenses.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDepenses)).BeginInit();
            this.pnlDepenses.SuspendLayout();
            this.tabInformateurs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInformateurs)).BeginInit();
            this.lblInformateurs.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcStats
            // 
            this.tcStats.Controls.Add(this.pageEquipe);
            this.tcStats.Controls.Add(this.pageFinances);
            this.tcStats.Controls.Add(this.pageExplorations);
            this.tcStats.Controls.Add(this.pageDepenses);
            this.tcStats.Controls.Add(this.tabInformateurs);
            this.tcStats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcStats.Location = new System.Drawing.Point(0, 0);
            this.tcStats.Name = "tcStats";
            this.tcStats.SelectedIndex = 0;
            this.tcStats.Size = new System.Drawing.Size(1835, 1230);
            this.tcStats.TabIndex = 1;
            // 
            // pageEquipe
            // 
            this.pageEquipe.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(45)))));
            this.pageEquipe.Controls.Add(this.dgvEquipe);
            this.pageEquipe.Controls.Add(this.pnlCbo);
            this.pageEquipe.ForeColor = System.Drawing.Color.White;
            this.pageEquipe.Location = new System.Drawing.Point(8, 59);
            this.pageEquipe.Name = "pageEquipe";
            this.pageEquipe.Padding = new System.Windows.Forms.Padding(3);
            this.pageEquipe.Size = new System.Drawing.Size(1819, 1163);
            this.pageEquipe.TabIndex = 0;
            this.pageEquipe.Text = "Équipes & réseau";
            // 
            // dgvEquipe
            // 
            this.dgvEquipe.AllowUserToAddRows = false;
            this.dgvEquipe.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvEquipe.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(45)))));
            this.dgvEquipe.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvEquipe.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(25)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvEquipe.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvEquipe.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEquipe.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Nom,
            this.Prénom,
            this.Type});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(45)))), ((int)(((byte)(55)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(100)))), ((int)(((byte)(150)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvEquipe.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvEquipe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvEquipe.EnableHeadersVisualStyles = false;
            this.dgvEquipe.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(75)))));
            this.dgvEquipe.Location = new System.Drawing.Point(3, 185);
            this.dgvEquipe.Name = "dgvEquipe";
            this.dgvEquipe.ReadOnly = true;
            this.dgvEquipe.RowHeadersVisible = false;
            this.dgvEquipe.RowHeadersWidth = 82;
            this.dgvEquipe.RowTemplate.Height = 33;
            this.dgvEquipe.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEquipe.Size = new System.Drawing.Size(1813, 975);
            this.dgvEquipe.TabIndex = 1;
            // 
            // Nom
            // 
            this.Nom.HeaderText = "Nom";
            this.Nom.MinimumWidth = 10;
            this.Nom.Name = "Nom";
            this.Nom.ReadOnly = true;
            // 
            // Prénom
            // 
            this.Prénom.HeaderText = "Prénom";
            this.Prénom.MinimumWidth = 10;
            this.Prénom.Name = "Prénom";
            this.Prénom.ReadOnly = true;
            // 
            // Type
            // 
            this.Type.HeaderText = "Type";
            this.Type.MinimumWidth = 10;
            this.Type.Name = "Type";
            this.Type.ReadOnly = true;
            // 
            // pnlCbo
            // 
            this.pnlCbo.Controls.Add(this.lblEquipe);
            this.pnlCbo.Controls.Add(this.cboNoms);
            this.pnlCbo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCbo.Location = new System.Drawing.Point(3, 3);
            this.pnlCbo.Name = "pnlCbo";
            this.pnlCbo.Size = new System.Drawing.Size(1813, 182);
            this.pnlCbo.TabIndex = 3;
            // 
            // lblEquipe
            // 
            this.lblEquipe.AutoSize = true;
            this.lblEquipe.Location = new System.Drawing.Point(3, 36);
            this.lblEquipe.Name = "lblEquipe";
            this.lblEquipe.Size = new System.Drawing.Size(938, 45);
            this.lblEquipe.TabIndex = 2;
            this.lblEquipe.Text = "Sélectionnez un membre pour voir ses compagnons de mission :";
            // 
            // cboNoms
            // 
            this.cboNoms.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(45)))), ((int)(((byte)(55)))));
            this.cboNoms.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNoms.FormattingEnabled = true;
            this.cboNoms.IntegralHeight = false;
            this.cboNoms.Location = new System.Drawing.Point(11, 84);
            this.cboNoms.Name = "cboNoms";
            this.cboNoms.Size = new System.Drawing.Size(930, 53);
            this.cboNoms.Sorted = true;
            this.cboNoms.TabIndex = 0;
            this.cboNoms.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cboNoms_DrawItem);
            this.cboNoms.SelectionChangeCommitted += new System.EventHandler(this.cboNoms_SelectionChangeCommitted);
            // 
            // pageFinances
            // 
            this.pageFinances.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(45)))));
            this.pageFinances.Controls.Add(this.dgvMissions);
            this.pageFinances.Controls.Add(this.pnlCartes);
            this.pageFinances.ForeColor = System.Drawing.Color.White;
            this.pageFinances.Location = new System.Drawing.Point(8, 59);
            this.pageFinances.Name = "pageFinances";
            this.pageFinances.Padding = new System.Windows.Forms.Padding(3);
            this.pageFinances.Size = new System.Drawing.Size(1819, 1163);
            this.pageFinances.TabIndex = 1;
            this.pageFinances.Text = "Budgets";
            // 
            // dgvMissions
            // 
            this.dgvMissions.AllowUserToAddRows = false;
            this.dgvMissions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMissions.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(45)))));
            this.dgvMissions.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvMissions.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(25)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMissions.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvMissions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMissions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.date,
            this.descriprtion,
            this.montant,
            this.categorie});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(45)))), ((int)(((byte)(55)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(100)))), ((int)(((byte)(150)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMissions.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvMissions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMissions.EnableHeadersVisualStyles = false;
            this.dgvMissions.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(75)))));
            this.dgvMissions.Location = new System.Drawing.Point(3, 496);
            this.dgvMissions.Name = "dgvMissions";
            this.dgvMissions.ReadOnly = true;
            this.dgvMissions.RowHeadersVisible = false;
            this.dgvMissions.RowHeadersWidth = 82;
            this.dgvMissions.RowTemplate.Height = 33;
            this.dgvMissions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMissions.Size = new System.Drawing.Size(1813, 664);
            this.dgvMissions.TabIndex = 6;
            // 
            // date
            // 
            this.date.HeaderText = "Date";
            this.date.MinimumWidth = 10;
            this.date.Name = "date";
            this.date.ReadOnly = true;
            // 
            // descriprtion
            // 
            this.descriprtion.HeaderText = "Description";
            this.descriprtion.MinimumWidth = 10;
            this.descriprtion.Name = "descriprtion";
            this.descriprtion.ReadOnly = true;
            // 
            // montant
            // 
            this.montant.HeaderText = "Montant";
            this.montant.MinimumWidth = 10;
            this.montant.Name = "montant";
            this.montant.ReadOnly = true;
            // 
            // categorie
            // 
            this.categorie.HeaderText = "Catégorie";
            this.categorie.MinimumWidth = 10;
            this.categorie.Name = "categorie";
            this.categorie.ReadOnly = true;
            // 
            // pnlCartes
            // 
            this.pnlCartes.Controls.Add(this.tlpMissions);
            this.pnlCartes.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCartes.Location = new System.Drawing.Point(3, 3);
            this.pnlCartes.Name = "pnlCartes";
            this.pnlCartes.Size = new System.Drawing.Size(1813, 493);
            this.pnlCartes.TabIndex = 5;
            // 
            // tlpMissions
            // 
            this.tlpMissions.ColumnCount = 3;
            this.tlpMissions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 1050F));
            this.tlpMissions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMissions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMissions.Controls.Add(this.pnlBudgetA, 2, 0);
            this.tlpMissions.Controls.Add(this.pnlC1, 0, 0);
            this.tlpMissions.Controls.Add(this.pnlBudget, 1, 0);
            this.tlpMissions.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpMissions.Location = new System.Drawing.Point(0, 0);
            this.tlpMissions.Name = "tlpMissions";
            this.tlpMissions.RowCount = 1;
            this.tlpMissions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMissions.Size = new System.Drawing.Size(1813, 420);
            this.tlpMissions.TabIndex = 0;
            // 
            // pnlBudgetA
            // 
            this.pnlBudgetA.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(45)))), ((int)(((byte)(55)))));
            this.pnlBudgetA.Controls.Add(this.pictureBox2);
            this.pnlBudgetA.Controls.Add(this.lblSommeA);
            this.pnlBudgetA.Controls.Add(this.lblBudgetA);
            this.pnlBudgetA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBudgetA.Location = new System.Drawing.Point(1441, 20);
            this.pnlBudgetA.Margin = new System.Windows.Forms.Padding(10, 20, 10, 0);
            this.pnlBudgetA.Name = "pnlBudgetA";
            this.pnlBudgetA.Size = new System.Drawing.Size(362, 400);
            this.pnlBudgetA.TabIndex = 2;
            this.pnlBudgetA.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlBudget_Paint);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.BackgroundImage = global::SAE24_Stargate.Properties.Resources.stonk;
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox2.Location = new System.Drawing.Point(53, 66);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(194, 273);
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            // 
            // lblSommeA
            // 
            this.lblSommeA.AutoSize = true;
            this.lblSommeA.Font = new System.Drawing.Font("Segoe UI", 25.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSommeA.Location = new System.Drawing.Point(37, 209);
            this.lblSommeA.Name = "lblSommeA";
            this.lblSommeA.Size = new System.Drawing.Size(0, 92);
            this.lblSommeA.TabIndex = 1;
            // 
            // lblBudgetA
            // 
            this.lblBudgetA.AutoSize = true;
            this.lblBudgetA.Font = new System.Drawing.Font("Segoe UI", 16.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBudgetA.Location = new System.Drawing.Point(45, 66);
            this.lblBudgetA.Name = "lblBudgetA";
            this.lblBudgetA.Size = new System.Drawing.Size(292, 59);
            this.lblBudgetA.TabIndex = 0;
            this.lblBudgetA.Text = "Budget Actuel";
            // 
            // pnlC1
            // 
            this.pnlC1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pnlC1.Controls.Add(this.grpMissions);
            this.pnlC1.Location = new System.Drawing.Point(28, 3);
            this.pnlC1.Name = "pnlC1";
            this.pnlC1.Size = new System.Drawing.Size(994, 414);
            this.pnlC1.TabIndex = 0;
            // 
            // grpMissions
            // 
            this.grpMissions.Controls.Add(this.cboMissions);
            this.grpMissions.ForeColor = System.Drawing.Color.White;
            this.grpMissions.Location = new System.Drawing.Point(0, 250);
            this.grpMissions.Name = "grpMissions";
            this.grpMissions.Size = new System.Drawing.Size(994, 164);
            this.grpMissions.TabIndex = 5;
            this.grpMissions.TabStop = false;
            this.grpMissions.Text = "Séléctionnez une mission pour afficher les dépenses";
            // 
            // cboMissions
            // 
            this.cboMissions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cboMissions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(45)))), ((int)(((byte)(55)))));
            this.cboMissions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMissions.FormattingEnabled = true;
            this.cboMissions.IntegralHeight = false;
            this.cboMissions.Location = new System.Drawing.Point(24, 62);
            this.cboMissions.Name = "cboMissions";
            this.cboMissions.Size = new System.Drawing.Size(930, 53);
            this.cboMissions.Sorted = true;
            this.cboMissions.TabIndex = 3;
            this.cboMissions.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cboNoms_DrawItem);
            this.cboMissions.SelectionChangeCommitted += new System.EventHandler(this.cboMissions_SelectionChangeCommitted);
            // 
            // pnlBudget
            // 
            this.pnlBudget.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(45)))), ((int)(((byte)(55)))));
            this.pnlBudget.Controls.Add(this.pictureBox1);
            this.pnlBudget.Controls.Add(this.lblSomme);
            this.pnlBudget.Controls.Add(this.lblBudgetI);
            this.pnlBudget.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBudget.Location = new System.Drawing.Point(1060, 20);
            this.pnlBudget.Margin = new System.Windows.Forms.Padding(10, 20, 10, 0);
            this.pnlBudget.Name = "pnlBudget";
            this.pnlBudget.Size = new System.Drawing.Size(361, 400);
            this.pnlBudget.TabIndex = 1;
            this.pnlBudget.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlBudget_Paint);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackgroundImage = global::SAE24_Stargate.Properties.Resources.calculator;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(53, 66);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(194, 273);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // lblSomme
            // 
            this.lblSomme.AutoSize = true;
            this.lblSomme.Font = new System.Drawing.Font("Segoe UI", 25.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSomme.Location = new System.Drawing.Point(53, 209);
            this.lblSomme.MinimumSize = new System.Drawing.Size(100, 100);
            this.lblSomme.Name = "lblSomme";
            this.lblSomme.Size = new System.Drawing.Size(100, 100);
            this.lblSomme.TabIndex = 1;
            // 
            // lblBudgetI
            // 
            this.lblBudgetI.AutoSize = true;
            this.lblBudgetI.Font = new System.Drawing.Font("Segoe UI", 16.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBudgetI.Location = new System.Drawing.Point(59, 66);
            this.lblBudgetI.Name = "lblBudgetI";
            this.lblBudgetI.Size = new System.Drawing.Size(275, 59);
            this.lblBudgetI.TabIndex = 0;
            this.lblBudgetI.Text = "Budget Initial";
            // 
            // pageExplorations
            // 
            this.pageExplorations.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(45)))));
            this.pageExplorations.Controls.Add(this.ccPlanetes);
            this.pageExplorations.Controls.Add(this.lblPlanetes);
            this.pageExplorations.Controls.Add(this.lblGraph);
            this.pageExplorations.ForeColor = System.Drawing.Color.White;
            this.pageExplorations.Location = new System.Drawing.Point(8, 59);
            this.pageExplorations.Name = "pageExplorations";
            this.pageExplorations.Padding = new System.Windows.Forms.Padding(3);
            this.pageExplorations.Size = new System.Drawing.Size(1819, 1163);
            this.pageExplorations.TabIndex = 2;
            this.pageExplorations.Text = "Explorations planétaires";
            // 
            // ccPlanetes
            // 
            this.ccPlanetes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ccPlanetes.Location = new System.Drawing.Point(3, 142);
            this.ccPlanetes.Name = "ccPlanetes";
            this.ccPlanetes.Size = new System.Drawing.Size(1813, 927);
            this.ccPlanetes.TabIndex = 0;
            this.ccPlanetes.Text = "elementHost1";
            this.ccPlanetes.Child = this.cartesianChart1;
            // 
            // lblPlanetes
            // 
            this.lblPlanetes.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblPlanetes.Font = new System.Drawing.Font("Segoe UI", 22.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlanetes.Location = new System.Drawing.Point(3, 1069);
            this.lblPlanetes.MinimumSize = new System.Drawing.Size(10, 10);
            this.lblPlanetes.Name = "lblPlanetes";
            this.lblPlanetes.Size = new System.Drawing.Size(1813, 91);
            this.lblPlanetes.TabIndex = 1;
            this.lblPlanetes.Text = "Planètes";
            this.lblPlanetes.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblGraph
            // 
            this.lblGraph.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblGraph.Font = new System.Drawing.Font("Segoe UI", 19.875F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGraph.Location = new System.Drawing.Point(3, 3);
            this.lblGraph.Name = "lblGraph";
            this.lblGraph.Size = new System.Drawing.Size(1813, 139);
            this.lblGraph.TabIndex = 2;
            this.lblGraph.Text = "Nombre de missions par planète";
            this.lblGraph.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pageDepenses
            // 
            this.pageDepenses.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(45)))));
            this.pageDepenses.Controls.Add(this.dgvDepenses);
            this.pageDepenses.Controls.Add(this.pnlDepenses);
            this.pageDepenses.ForeColor = System.Drawing.Color.White;
            this.pageDepenses.Location = new System.Drawing.Point(8, 59);
            this.pageDepenses.Name = "pageDepenses";
            this.pageDepenses.Padding = new System.Windows.Forms.Padding(3);
            this.pageDepenses.Size = new System.Drawing.Size(1819, 1163);
            this.pageDepenses.TabIndex = 3;
            this.pageDepenses.Text = "Dépenses";
            // 
            // dgvDepenses
            // 
            this.dgvDepenses.AllowUserToAddRows = false;
            this.dgvDepenses.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDepenses.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(45)))));
            this.dgvDepenses.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvDepenses.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(25)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDepenses.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvDepenses.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDepenses.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.depenses,
            this.nomMission,
            this.nomChef});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(45)))), ((int)(((byte)(55)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(100)))), ((int)(((byte)(150)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDepenses.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvDepenses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDepenses.EnableHeadersVisualStyles = false;
            this.dgvDepenses.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(75)))));
            this.dgvDepenses.Location = new System.Drawing.Point(3, 280);
            this.dgvDepenses.Name = "dgvDepenses";
            this.dgvDepenses.ReadOnly = true;
            this.dgvDepenses.RowHeadersVisible = false;
            this.dgvDepenses.RowHeadersWidth = 82;
            this.dgvDepenses.RowTemplate.Height = 33;
            this.dgvDepenses.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDepenses.Size = new System.Drawing.Size(1813, 880);
            this.dgvDepenses.TabIndex = 2;
            // 
            // depenses
            // 
            this.depenses.DataPropertyName = "depenses";
            this.depenses.HeaderText = "Dépenses la plus importante";
            this.depenses.MinimumWidth = 10;
            this.depenses.Name = "depenses";
            this.depenses.ReadOnly = true;
            // 
            // nomMission
            // 
            this.nomMission.DataPropertyName = "nomMission";
            this.nomMission.HeaderText = "Nom de la mission";
            this.nomMission.MinimumWidth = 10;
            this.nomMission.Name = "nomMission";
            this.nomMission.ReadOnly = true;
            // 
            // nomChef
            // 
            this.nomChef.DataPropertyName = "nomChef";
            this.nomChef.HeaderText = "Chef de mission";
            this.nomChef.MinimumWidth = 10;
            this.nomChef.Name = "nomChef";
            this.nomChef.ReadOnly = true;
            // 
            // pnlDepenses
            // 
            this.pnlDepenses.Controls.Add(this.lblTitre);
            this.pnlDepenses.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDepenses.Location = new System.Drawing.Point(3, 3);
            this.pnlDepenses.Name = "pnlDepenses";
            this.pnlDepenses.Size = new System.Drawing.Size(1813, 277);
            this.pnlDepenses.TabIndex = 3;
            // 
            // lblTitre
            // 
            this.lblTitre.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitre.Font = new System.Drawing.Font("Segoe UI", 19.875F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitre.Location = new System.Drawing.Point(0, 0);
            this.lblTitre.MinimumSize = new System.Drawing.Size(100, 10);
            this.lblTitre.Name = "lblTitre";
            this.lblTitre.Size = new System.Drawing.Size(1813, 277);
            this.lblTitre.TabIndex = 0;
            this.lblTitre.Text = "Dépenses Maximales par Mission";
            this.lblTitre.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabInformateurs
            // 
            this.tabInformateurs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(45)))));
            this.tabInformateurs.Controls.Add(this.dgvInformateurs);
            this.tabInformateurs.Controls.Add(this.lblInformateurs);
            this.tabInformateurs.Location = new System.Drawing.Point(8, 59);
            this.tabInformateurs.Name = "tabInformateurs";
            this.tabInformateurs.Size = new System.Drawing.Size(1819, 1163);
            this.tabInformateurs.TabIndex = 4;
            this.tabInformateurs.Text = "Informateurs";
            // 
            // dgvInformateurs
            // 
            this.dgvInformateurs.AllowUserToAddRows = false;
            this.dgvInformateurs.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvInformateurs.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(45)))));
            this.dgvInformateurs.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvInformateurs.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(25)))));
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvInformateurs.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvInformateurs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInformateurs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nomCode,
            this.nomEspece,
            this.somme});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(45)))), ((int)(((byte)(55)))));
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(100)))), ((int)(((byte)(150)))));
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvInformateurs.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgvInformateurs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvInformateurs.EnableHeadersVisualStyles = false;
            this.dgvInformateurs.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(75)))));
            this.dgvInformateurs.Location = new System.Drawing.Point(0, 292);
            this.dgvInformateurs.Name = "dgvInformateurs";
            this.dgvInformateurs.ReadOnly = true;
            this.dgvInformateurs.RowHeadersVisible = false;
            this.dgvInformateurs.RowHeadersWidth = 82;
            this.dgvInformateurs.RowTemplate.Height = 33;
            this.dgvInformateurs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvInformateurs.Size = new System.Drawing.Size(1819, 871);
            this.dgvInformateurs.TabIndex = 3;
            // 
            // nomCode
            // 
            this.nomCode.DataPropertyName = "nomCodeInformateur";
            this.nomCode.HeaderText = "Nom de code";
            this.nomCode.MinimumWidth = 10;
            this.nomCode.Name = "nomCode";
            this.nomCode.ReadOnly = true;
            // 
            // nomEspece
            // 
            this.nomEspece.DataPropertyName = "nom";
            this.nomEspece.HeaderText = "Espèce d\'origine";
            this.nomEspece.MinimumWidth = 10;
            this.nomEspece.Name = "nomEspece";
            this.nomEspece.ReadOnly = true;
            // 
            // somme
            // 
            this.somme.DataPropertyName = "Somme versée";
            this.somme.HeaderText = "Somme totale reçue";
            this.somme.MinimumWidth = 10;
            this.somme.Name = "somme";
            this.somme.ReadOnly = true;
            // 
            // lblInformateurs
            // 
            this.lblInformateurs.Controls.Add(this.groupBox1);
            this.lblInformateurs.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblInformateurs.Location = new System.Drawing.Point(0, 0);
            this.lblInformateurs.Name = "lblInformateurs";
            this.lblInformateurs.Size = new System.Drawing.Size(1819, 292);
            this.lblInformateurs.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cboInformateurs);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(17, 99);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1232, 169);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Sélectionnez une mission pour afficher l\'informateur qui a reçu le moins d\'argent" +
    "";
            // 
            // cboInformateurs
            // 
            this.cboInformateurs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cboInformateurs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(45)))), ((int)(((byte)(55)))));
            this.cboInformateurs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboInformateurs.FormattingEnabled = true;
            this.cboInformateurs.IntegralHeight = false;
            this.cboInformateurs.Location = new System.Drawing.Point(24, 76);
            this.cboInformateurs.Name = "cboInformateurs";
            this.cboInformateurs.Size = new System.Drawing.Size(1142, 53);
            this.cboInformateurs.Sorted = true;
            this.cboInformateurs.TabIndex = 3;
            this.cboInformateurs.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cboNoms_DrawItem);
            this.cboInformateurs.SelectionChangeCommitted += new System.EventHandler(this.cboInformateurs_SelectionChangeCommitted);
            // 
            // ucVueStatistiques
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(64)))), ((int)(((byte)(66)))));
            this.Controls.Add(this.tcStats);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ucVueStatistiques";
            this.Size = new System.Drawing.Size(1835, 1230);
            this.Load += new System.EventHandler(this.ucVueStatistiques_Load);
            this.tcStats.ResumeLayout(false);
            this.pageEquipe.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEquipe)).EndInit();
            this.pnlCbo.ResumeLayout(false);
            this.pnlCbo.PerformLayout();
            this.pageFinances.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMissions)).EndInit();
            this.pnlCartes.ResumeLayout(false);
            this.tlpMissions.ResumeLayout(false);
            this.pnlBudgetA.ResumeLayout(false);
            this.pnlBudgetA.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.pnlC1.ResumeLayout(false);
            this.grpMissions.ResumeLayout(false);
            this.pnlBudget.ResumeLayout(false);
            this.pnlBudget.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.pageExplorations.ResumeLayout(false);
            this.pageDepenses.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDepenses)).EndInit();
            this.pnlDepenses.ResumeLayout(false);
            this.tabInformateurs.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInformateurs)).EndInit();
            this.lblInformateurs.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabControl tcStats;
        private System.Windows.Forms.TabPage pageEquipe;
        private System.Windows.Forms.TabPage pageFinances;
        private System.Windows.Forms.TabPage pageExplorations;
        private System.Windows.Forms.TabPage pageDepenses;
        private System.Windows.Forms.ComboBox cboNoms;
        private System.Windows.Forms.DataGridView dgvEquipe;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nom;
        private System.Windows.Forms.DataGridViewTextBoxColumn Prénom;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type;
        private System.Windows.Forms.Label lblEquipe;
        private System.Windows.Forms.Panel pnlCbo;
        private System.Windows.Forms.ComboBox cboMissions;
        private System.Windows.Forms.Panel pnlCartes;
        private System.Windows.Forms.TableLayoutPanel tlpMissions;
        private System.Windows.Forms.Panel pnlC1;
        private System.Windows.Forms.GroupBox grpMissions;
        private System.Windows.Forms.Panel pnlBudget;
        private System.Windows.Forms.Label lblBudgetI;
        private System.Windows.Forms.Label lblSomme;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel pnlBudgetA;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label lblSommeA;
        private System.Windows.Forms.Label lblBudgetA;
        private System.Windows.Forms.DataGridView dgvMissions;
        private System.Windows.Forms.DataGridViewTextBoxColumn date;
        private System.Windows.Forms.DataGridViewTextBoxColumn descriprtion;
        private System.Windows.Forms.DataGridViewTextBoxColumn montant;
        private System.Windows.Forms.DataGridViewTextBoxColumn categorie;
        private System.Windows.Forms.Integration.ElementHost ccPlanetes;
        private LiveCharts.Wpf.CartesianChart cartesianChart1;
        private System.Windows.Forms.Label lblPlanetes;
        private System.Windows.Forms.DataGridView dgvDepenses;
        private System.Windows.Forms.Panel pnlDepenses;
        private System.Windows.Forms.Label lblTitre;
        private System.Windows.Forms.Label lblGraph;
        private System.Windows.Forms.TabPage tabInformateurs;
        private System.Windows.Forms.DataGridViewTextBoxColumn depenses;
        private System.Windows.Forms.DataGridViewTextBoxColumn nomMission;
        private System.Windows.Forms.DataGridViewTextBoxColumn nomChef;
        private System.Windows.Forms.Panel lblInformateurs;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cboInformateurs;
        private System.Windows.Forms.DataGridView dgvInformateurs;
        private System.Windows.Forms.DataGridViewTextBoxColumn nomCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn nomEspece;
        private System.Windows.Forms.DataGridViewTextBoxColumn somme;
    }
}
