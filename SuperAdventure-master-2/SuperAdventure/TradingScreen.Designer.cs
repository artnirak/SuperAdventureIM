namespace SuperAdventure
{
    partial class TradingScreen
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lvlMyInventory = new System.Windows.Forms.Label();
            this.lblVendorInventory = new System.Windows.Forms.Label();
            this.dgvMyItems = new System.Windows.Forms.DataGridView();
            this.dgvVendorItems = new System.Windows.Forms.DataGridView();
            this.btnClose = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblVendorGold = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMyItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVendorItems)).BeginInit();
            this.SuspendLayout();
            // 
            // lvlMyInventory
            // 
            this.lvlMyInventory.AutoSize = true;
            this.lvlMyInventory.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvlMyInventory.Location = new System.Drawing.Point(74, 11);
            this.lvlMyInventory.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lvlMyInventory.Name = "lvlMyInventory";
            this.lvlMyInventory.Size = new System.Drawing.Size(89, 11);
            this.lvlMyInventory.TabIndex = 0;
            this.lvlMyInventory.Text = "My Inventory";
            // 
            // lblVendorInventory
            // 
            this.lblVendorInventory.AutoSize = true;
            this.lblVendorInventory.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVendorInventory.Location = new System.Drawing.Point(262, 11);
            this.lblVendorInventory.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblVendorInventory.Name = "lblVendorInventory";
            this.lblVendorInventory.Size = new System.Drawing.Size(131, 11);
            this.lblVendorInventory.TabIndex = 1;
            this.lblVendorInventory.Text = "Vendor\'s Inventory";
            // 
            // dgvMyItems
            // 
            this.dgvMyItems.AllowUserToAddRows = false;
            this.dgvMyItems.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMyItems.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvMyItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMyItems.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvMyItems.Location = new System.Drawing.Point(10, 35);
            this.dgvMyItems.Margin = new System.Windows.Forms.Padding(2);
            this.dgvMyItems.Name = "dgvMyItems";
            this.dgvMyItems.ReadOnly = true;
            this.dgvMyItems.RowTemplate.Height = 24;
            this.dgvMyItems.Size = new System.Drawing.Size(210, 176);
            this.dgvMyItems.TabIndex = 2;
            // 
            // dgvVendorItems
            // 
            this.dgvVendorItems.AllowUserToAddRows = false;
            this.dgvVendorItems.AllowUserToDeleteRows = false;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvVendorItems.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvVendorItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvVendorItems.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvVendorItems.Location = new System.Drawing.Point(230, 35);
            this.dgvVendorItems.Margin = new System.Windows.Forms.Padding(2);
            this.dgvVendorItems.Name = "dgvVendorItems";
            this.dgvVendorItems.ReadOnly = true;
            this.dgvVendorItems.RowTemplate.Height = 24;
            this.dgvVendorItems.Size = new System.Drawing.Size(202, 176);
            this.dgvVendorItems.TabIndex = 3;
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(376, 223);
            this.btnClose.Margin = new System.Windows.Forms.Padding(2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(56, 19);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(227, 223);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Vendor Gold:";
            // 
            // lblVendorGold
            // 
            this.lblVendorGold.AutoSize = true;
            this.lblVendorGold.Location = new System.Drawing.Point(321, 223);
            this.lblVendorGold.Name = "lblVendorGold";
            this.lblVendorGold.Size = new System.Drawing.Size(0, 13);
            this.lblVendorGold.TabIndex = 6;
            // 
            // TradingScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(440, 245);
            this.Controls.Add(this.lblVendorGold);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dgvVendorItems);
            this.Controls.Add(this.dgvMyItems);
            this.Controls.Add(this.lblVendorInventory);
            this.Controls.Add(this.lvlMyInventory);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "TradingScreen";
            this.Text = "Trade";
            ((System.ComponentModel.ISupportInitialize)(this.dgvMyItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVendorItems)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lvlMyInventory;
        private System.Windows.Forms.Label lblVendorInventory;
        private System.Windows.Forms.DataGridView dgvMyItems;
        private System.Windows.Forms.DataGridView dgvVendorItems;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblVendorGold;
    }
}