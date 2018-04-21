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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.lvlMyInventory.Location = new System.Drawing.Point(105, 11);
            this.lvlMyInventory.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lvlMyInventory.Name = "lvlMyInventory";
            this.lvlMyInventory.Size = new System.Drawing.Size(103, 11);
            this.lvlMyInventory.TabIndex = 0;
            this.lvlMyInventory.Text = "Meu inventário";
            // 
            // lblVendorInventory
            // 
            this.lblVendorInventory.AutoSize = true;
            this.lblVendorInventory.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVendorInventory.Location = new System.Drawing.Point(402, 11);
            this.lblVendorInventory.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblVendorInventory.Name = "lblVendorInventory";
            this.lblVendorInventory.Size = new System.Drawing.Size(159, 11);
            this.lblVendorInventory.TabIndex = 1;
            this.lblVendorInventory.Text = "Inventário do vendedor";
            // 
            // dgvMyItems
            // 
            this.dgvMyItems.AllowUserToAddRows = false;
            this.dgvMyItems.AllowUserToDeleteRows = false;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMyItems.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvMyItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMyItems.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvMyItems.Location = new System.Drawing.Point(10, 35);
            this.dgvMyItems.Margin = new System.Windows.Forms.Padding(2);
            this.dgvMyItems.Name = "dgvMyItems";
            this.dgvMyItems.ReadOnly = true;
            this.dgvMyItems.RowTemplate.Height = 24;
            this.dgvMyItems.Size = new System.Drawing.Size(311, 246);
            this.dgvMyItems.TabIndex = 2;
            // 
            // dgvVendorItems
            // 
            this.dgvVendorItems.AllowUserToAddRows = false;
            this.dgvVendorItems.AllowUserToDeleteRows = false;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvVendorItems.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvVendorItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvVendorItems.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgvVendorItems.Location = new System.Drawing.Point(339, 35);
            this.dgvVendorItems.Margin = new System.Windows.Forms.Padding(2);
            this.dgvVendorItems.Name = "dgvVendorItems";
            this.dgvVendorItems.ReadOnly = true;
            this.dgvVendorItems.RowTemplate.Height = 24;
            this.dgvVendorItems.Size = new System.Drawing.Size(333, 246);
            this.dgvVendorItems.TabIndex = 3;
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(616, 289);
            this.btnClose.Margin = new System.Windows.Forms.Padding(2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(56, 19);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "Fechar";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(336, 295);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Ouro do vendedor:";
            // 
            // lblVendorGold
            // 
            this.lblVendorGold.AutoSize = true;
            this.lblVendorGold.Location = new System.Drawing.Point(430, 295);
            this.lblVendorGold.Name = "lblVendorGold";
            this.lblVendorGold.Size = new System.Drawing.Size(0, 13);
            this.lblVendorGold.TabIndex = 6;
            // 
            // TradingScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(683, 315);
            this.Controls.Add(this.lblVendorGold);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dgvVendorItems);
            this.Controls.Add(this.dgvMyItems);
            this.Controls.Add(this.lblVendorInventory);
            this.Controls.Add(this.lvlMyInventory);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "TradingScreen";
            this.Text = "Negócio";
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