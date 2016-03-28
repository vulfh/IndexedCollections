namespace Demo
{
    partial class Demo
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
            this.grpAdd = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtAddId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtAddFirstName = new System.Windows.Forms.TextBox();
            this.txtAddLastName = new System.Windows.Forms.TextBox();
            this.grpFetch = new System.Windows.Forms.GroupBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.txtFetchId = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lstResult = new System.Windows.Forms.ListBox();
            this.btnFetch = new System.Windows.Forms.Button();
            this.txtFetchFirstName = new System.Windows.Forms.TextBox();
            this.txtFetchLastName = new System.Windows.Forms.TextBox();
            this.grpAdd.SuspendLayout();
            this.grpFetch.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpAdd
            // 
            this.grpAdd.Controls.Add(this.label6);
            this.grpAdd.Controls.Add(this.txtAddId);
            this.grpAdd.Controls.Add(this.label2);
            this.grpAdd.Controls.Add(this.label1);
            this.grpAdd.Controls.Add(this.btnAdd);
            this.grpAdd.Controls.Add(this.txtAddFirstName);
            this.grpAdd.Controls.Add(this.txtAddLastName);
            this.grpAdd.Location = new System.Drawing.Point(48, 29);
            this.grpAdd.Name = "grpAdd";
            this.grpAdd.Size = new System.Drawing.Size(979, 115);
            this.grpAdd.TabIndex = 1;
            this.grpAdd.TabStop = false;
            this.grpAdd.Text = "Add Person";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label6.Location = new System.Drawing.Point(12, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 24);
            this.label6.TabIndex = 6;
            this.label6.Text = "ID";
            // 
            // txtAddId
            // 
            this.txtAddId.Location = new System.Drawing.Point(164, 21);
            this.txtAddId.Name = "txtAddId";
            this.txtAddId.Size = new System.Drawing.Size(188, 22);
            this.txtAddId.TabIndex = 0;
            this.txtAddId.TextChanged += new System.EventHandler(this.txtAddId_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label2.Location = new System.Drawing.Point(387, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 24);
            this.label2.TabIndex = 4;
            this.label2.Text = "First name";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label1.Location = new System.Drawing.Point(12, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 24);
            this.label1.TabIndex = 3;
            this.label1.Text = "Last name";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(815, 71);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtAddFirstName
            // 
            this.txtAddFirstName.Location = new System.Drawing.Point(557, 72);
            this.txtAddFirstName.Name = "txtAddFirstName";
            this.txtAddFirstName.Size = new System.Drawing.Size(188, 22);
            this.txtAddFirstName.TabIndex = 2;
            // 
            // txtAddLastName
            // 
            this.txtAddLastName.Location = new System.Drawing.Point(164, 72);
            this.txtAddLastName.Name = "txtAddLastName";
            this.txtAddLastName.Size = new System.Drawing.Size(188, 22);
            this.txtAddLastName.TabIndex = 1;
            // 
            // grpFetch
            // 
            this.grpFetch.Controls.Add(this.btnDelete);
            this.grpFetch.Controls.Add(this.label7);
            this.grpFetch.Controls.Add(this.txtFetchId);
            this.grpFetch.Controls.Add(this.label5);
            this.grpFetch.Controls.Add(this.label4);
            this.grpFetch.Controls.Add(this.label3);
            this.grpFetch.Controls.Add(this.lstResult);
            this.grpFetch.Controls.Add(this.btnFetch);
            this.grpFetch.Controls.Add(this.txtFetchFirstName);
            this.grpFetch.Controls.Add(this.txtFetchLastName);
            this.grpFetch.Location = new System.Drawing.Point(48, 184);
            this.grpFetch.Name = "grpFetch";
            this.grpFetch.Size = new System.Drawing.Size(979, 232);
            this.grpFetch.TabIndex = 2;
            this.grpFetch.TabStop = false;
            this.grpFetch.Text = "Fetch Person";
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(722, 203);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 10;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label7.Location = new System.Drawing.Point(21, 37);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 24);
            this.label7.TabIndex = 9;
            this.label7.Text = "ID";
            // 
            // txtFetchId
            // 
            this.txtFetchId.Location = new System.Drawing.Point(173, 37);
            this.txtFetchId.Name = "txtFetchId";
            this.txtFetchId.Size = new System.Drawing.Size(188, 22);
            this.txtFetchId.TabIndex = 4;
            this.txtFetchId.TextChanged += new System.EventHandler(this.txtFetchId_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label5.Location = new System.Drawing.Point(537, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(116, 24);
            this.label5.TabIndex = 7;
            this.label5.Text = "Found data";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label4.Location = new System.Drawing.Point(21, 105);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 24);
            this.label4.TabIndex = 6;
            this.label4.Text = "Last name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label3.Location = new System.Drawing.Point(18, 190);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 24);
            this.label3.TabIndex = 5;
            this.label3.Text = "First name";
            // 
            // lstResult
            // 
            this.lstResult.FormattingEnabled = true;
            this.lstResult.ItemHeight = 16;
            this.lstResult.Location = new System.Drawing.Point(541, 64);
            this.lstResult.Name = "lstResult";
            this.lstResult.Size = new System.Drawing.Size(349, 132);
            this.lstResult.TabIndex = 8;
            // 
            // btnFetch
            // 
            this.btnFetch.Location = new System.Drawing.Point(815, 202);
            this.btnFetch.Name = "btnFetch";
            this.btnFetch.Size = new System.Drawing.Size(75, 23);
            this.btnFetch.TabIndex = 7;
            this.btnFetch.Text = "Find";
            this.btnFetch.UseVisualStyleBackColor = true;
            this.btnFetch.Click += new System.EventHandler(this.btnFetch_Click);
            // 
            // txtFetchFirstName
            // 
            this.txtFetchFirstName.Location = new System.Drawing.Point(173, 190);
            this.txtFetchFirstName.Name = "txtFetchFirstName";
            this.txtFetchFirstName.Size = new System.Drawing.Size(188, 22);
            this.txtFetchFirstName.TabIndex = 6;
            // 
            // txtFetchLastName
            // 
            this.txtFetchLastName.Location = new System.Drawing.Point(173, 105);
            this.txtFetchLastName.Name = "txtFetchLastName";
            this.txtFetchLastName.Size = new System.Drawing.Size(188, 22);
            this.txtFetchLastName.TabIndex = 5;
            // 
            // Demo
            // 
            this.AcceptButton = this.btnAdd;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1058, 470);
            this.Controls.Add(this.grpFetch);
            this.Controls.Add(this.grpAdd);
            this.Name = "Demo";
            this.Text = "Indexed Dictionary Demo";
            this.Load += new System.EventHandler(this.Demo_Load);
            this.grpAdd.ResumeLayout(false);
            this.grpAdd.PerformLayout();
            this.grpFetch.ResumeLayout(false);
            this.grpFetch.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpAdd;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox txtAddFirstName;
        private System.Windows.Forms.TextBox txtAddLastName;
        private System.Windows.Forms.GroupBox grpFetch;
        private System.Windows.Forms.ListBox lstResult;
        private System.Windows.Forms.Button btnFetch;
        private System.Windows.Forms.TextBox txtFetchFirstName;
        private System.Windows.Forms.TextBox txtFetchLastName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtAddId;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtFetchId;
        private System.Windows.Forms.Button btnDelete;
    }
}

