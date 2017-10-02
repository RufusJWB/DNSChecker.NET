namespace CheckerUI
{
    partial class MainWindow
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.DomainsToCheck = new System.Windows.Forms.TextBox();
            this.CheckDomains = new System.Windows.Forms.Button();
            this.SOARecords = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CAARecords = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.CheckResults = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // DomainsToCheck
            // 
            this.DomainsToCheck.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DomainsToCheck.Location = new System.Drawing.Point(12, 12);
            this.DomainsToCheck.Multiline = true;
            this.DomainsToCheck.Name = "DomainsToCheck";
            this.DomainsToCheck.Size = new System.Drawing.Size(662, 102);
            this.DomainsToCheck.TabIndex = 0;
            // 
            // CheckDomains
            // 
            this.CheckDomains.Location = new System.Drawing.Point(12, 120);
            this.CheckDomains.Name = "CheckDomains";
            this.CheckDomains.Size = new System.Drawing.Size(75, 23);
            this.CheckDomains.TabIndex = 1;
            this.CheckDomains.Text = "Go check";
            this.CheckDomains.UseVisualStyleBackColor = true;
            this.CheckDomains.Click += new System.EventHandler(this.CheckDomains_Click);
            // 
            // SOARecords
            // 
            this.SOARecords.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SOARecords.Location = new System.Drawing.Point(76, 149);
            this.SOARecords.Name = "SOARecords";
            this.SOARecords.Size = new System.Drawing.Size(598, 20);
            this.SOARecords.TabIndex = 2;
            this.SOARecords.Text = "siemens.net, siemens.com, siemens.de";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 152);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "SOA TLDs";
            // 
            // CAARecords
            // 
            this.CAARecords.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CAARecords.Location = new System.Drawing.Point(76, 175);
            this.CAARecords.Name = "CAARecords";
            this.CAARecords.Size = new System.Drawing.Size(598, 20);
            this.CAARecords.TabIndex = 4;
            this.CAARecords.Text = "quovadisglobal.com, pki.siemens.com";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 178);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "CAAs";
            // 
            // CheckResults
            // 
            this.CheckResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CheckResults.Location = new System.Drawing.Point(12, 201);
            this.CheckResults.Multiline = true;
            this.CheckResults.Name = "CheckResults";
            this.CheckResults.Size = new System.Drawing.Size(662, 181);
            this.CheckResults.TabIndex = 6;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(686, 394);
            this.Controls.Add(this.CheckResults);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.CAARecords);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SOARecords);
            this.Controls.Add(this.CheckDomains);
            this.Controls.Add(this.DomainsToCheck);
            this.Name = "MainWindow";
            this.Text = "DNS SOA CAA Checker";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox DomainsToCheck;
        private System.Windows.Forms.Button CheckDomains;
        private System.Windows.Forms.TextBox SOARecords;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox CAARecords;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox CheckResults;
    }
}

