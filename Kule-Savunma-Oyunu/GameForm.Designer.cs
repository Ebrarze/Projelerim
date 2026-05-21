namespace Kule_savunma_oyunu
{
    partial class GameForm
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panelTop = new System.Windows.Forms.Panel();
            this.lblSkor = new System.Windows.Forms.Label();
            this.lblDalga = new System.Windows.Forms.Label();
            this.lblCan = new System.Windows.Forms.Label();
            this.lblAltin = new System.Windows.Forms.Label();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.panelHUD = new System.Windows.Forms.Panel();
            this.btnYukseltBuyu = new System.Windows.Forms.Button();
            this.btnBuyu = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnYukseltTop = new System.Windows.Forms.Button();
            this.btnYukseltOk = new System.Windows.Forms.Button();
            this.btnTop = new System.Windows.Forms.Button();
            this.panelGame = new System.Windows.Forms.Panel();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.panelTop.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.panelHUD.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.Lavender;
            this.panelTop.Controls.Add(this.lblSkor);
            this.panelTop.Controls.Add(this.lblDalga);
            this.panelTop.Controls.Add(this.lblCan);
            this.panelTop.Controls.Add(this.lblAltin);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(977, 60);
            this.panelTop.TabIndex = 0;
            // 
            // lblSkor
            // 
            this.lblSkor.AutoSize = true;
            this.lblSkor.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSkor.Location = new System.Drawing.Point(380, 20);
            this.lblSkor.Name = "lblSkor";
            this.lblSkor.Size = new System.Drawing.Size(63, 23);
            this.lblSkor.TabIndex = 3;
            this.lblSkor.Text = "Skor:0";
            // 
            // lblDalga
            // 
            this.lblDalga.AutoSize = true;
            this.lblDalga.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDalga.ForeColor = System.Drawing.Color.DeepPink;
            this.lblDalga.Location = new System.Drawing.Point(260, 20);
            this.lblDalga.Name = "lblDalga";
            this.lblDalga.Size = new System.Drawing.Size(72, 23);
            this.lblDalga.TabIndex = 2;
            this.lblDalga.Text = "Dalga:1";
            // 
            // lblCan
            // 
            this.lblCan.AutoSize = true;
            this.lblCan.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCan.ForeColor = System.Drawing.Color.SeaGreen;
            this.lblCan.Location = new System.Drawing.Point(150, 20);
            this.lblCan.Name = "lblCan";
            this.lblCan.Size = new System.Drawing.Size(55, 23);
            this.lblCan.TabIndex = 1;
            this.lblCan.Text = "Can:0";
            // 
            // lblAltin
            // 
            this.lblAltin.AutoSize = true;
            this.lblAltin.BackColor = System.Drawing.Color.Lavender;
            this.lblAltin.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAltin.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblAltin.Location = new System.Drawing.Point(20, 20);
            this.lblAltin.Name = "lblAltin";
            this.lblAltin.Size = new System.Drawing.Size(64, 23);
            this.lblAltin.TabIndex = 0;
            this.lblAltin.Text = "Altın:0";
            // 
            // panelBottom
            // 
            this.panelBottom.BackColor = System.Drawing.Color.Lavender;
            this.panelBottom.Controls.Add(this.panelHUD);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 465);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(977, 100);
            this.panelBottom.TabIndex = 1;
            // 
            // panelHUD
            // 
            this.panelHUD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelHUD.Controls.Add(this.btnYukseltBuyu);
            this.panelHUD.Controls.Add(this.btnBuyu);
            this.panelHUD.Controls.Add(this.btnOk);
            this.panelHUD.Controls.Add(this.btnYukseltTop);
            this.panelHUD.Controls.Add(this.btnYukseltOk);
            this.panelHUD.Controls.Add(this.btnTop);
            this.panelHUD.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelHUD.Location = new System.Drawing.Point(0, 10);
            this.panelHUD.Name = "panelHUD";
            this.panelHUD.Size = new System.Drawing.Size(977, 90);
            this.panelHUD.TabIndex = 0;
            this.panelHUD.Paint += new System.Windows.Forms.PaintEventHandler(this.panelHUD_Paint);
            // 
            // btnYukseltBuyu
            // 
            this.btnYukseltBuyu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnYukseltBuyu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnYukseltBuyu.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnYukseltBuyu.Location = new System.Drawing.Point(732, 24);
            this.btnYukseltBuyu.Margin = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.btnYukseltBuyu.Name = "btnYukseltBuyu";
            this.btnYukseltBuyu.Size = new System.Drawing.Size(74, 34);
            this.btnYukseltBuyu.TabIndex = 5;
            this.btnYukseltBuyu.Text = "Yükselt";
            this.btnYukseltBuyu.UseCompatibleTextRendering = true;
            this.btnYukseltBuyu.UseVisualStyleBackColor = false;
            // 
            // btnBuyu
            // 
            this.btnBuyu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnBuyu.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnBuyu.ForeColor = System.Drawing.Color.White;
            this.btnBuyu.Location = new System.Drawing.Point(566, 15);
            this.btnBuyu.Margin = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.btnBuyu.Name = "btnBuyu";
            this.btnBuyu.Size = new System.Drawing.Size(164, 50);
            this.btnBuyu.TabIndex = 2;
            this.btnBuyu.Text = "Büyü Kulesi(200)";
            this.btnBuyu.UseCompatibleTextRendering = true;
            this.btnBuyu.UseVisualStyleBackColor = false;
            this.btnBuyu.Click += new System.EventHandler(this.btnBuyu_Click);
            // 
            // btnOk
            // 
            this.btnOk.BackColor = System.Drawing.Color.Navy;
            this.btnOk.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.ForeColor = System.Drawing.Color.White;
            this.btnOk.Location = new System.Drawing.Point(14, 15);
            this.btnOk.Margin = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(149, 59);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "Ok  Kulesi (100)";
            this.btnOk.UseCompatibleTextRendering = true;
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.VisibleChanged += new System.EventHandler(this.btnOk_Click);
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnYukseltTop
            // 
            this.btnYukseltTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnYukseltTop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnYukseltTop.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnYukseltTop.Location = new System.Drawing.Point(448, 23);
            this.btnYukseltTop.Margin = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.btnYukseltTop.Name = "btnYukseltTop";
            this.btnYukseltTop.Size = new System.Drawing.Size(69, 34);
            this.btnYukseltTop.TabIndex = 4;
            this.btnYukseltTop.Text = "Yükselt";
            this.btnYukseltTop.UseCompatibleTextRendering = true;
            this.btnYukseltTop.UseVisualStyleBackColor = false;
            // 
            // btnYukseltOk
            // 
            this.btnYukseltOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnYukseltOk.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnYukseltOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnYukseltOk.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.btnYukseltOk.Location = new System.Drawing.Point(164, 27);
            this.btnYukseltOk.Margin = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.btnYukseltOk.Name = "btnYukseltOk";
            this.btnYukseltOk.Size = new System.Drawing.Size(79, 34);
            this.btnYukseltOk.TabIndex = 3;
            this.btnYukseltOk.Text = "Yükselt";
            this.btnYukseltOk.UseCompatibleTextRendering = true;
            this.btnYukseltOk.UseVisualStyleBackColor = false;
            this.btnYukseltOk.Click += new System.EventHandler(this.btnYukselt_Click);
            // 
            // btnTop
            // 
            this.btnTop.BackColor = System.Drawing.Color.Red;
            this.btnTop.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnTop.ForeColor = System.Drawing.Color.White;
            this.btnTop.Location = new System.Drawing.Point(294, 15);
            this.btnTop.Margin = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.btnTop.Name = "btnTop";
            this.btnTop.Size = new System.Drawing.Size(152, 53);
            this.btnTop.TabIndex = 1;
            this.btnTop.Text = "Top Kulesi (250)";
            this.btnTop.UseCompatibleTextRendering = true;
            this.btnTop.UseVisualStyleBackColor = false;
            this.btnTop.Click += new System.EventHandler(this.btnTop_Click);
            // 
            // panelGame
            // 
            this.panelGame.BackColor = System.Drawing.Color.White;
            this.panelGame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.panelGame.ForeColor = System.Drawing.Color.White;
            this.panelGame.Location = new System.Drawing.Point(0, 60);
            this.panelGame.Name = "panelGame";
            this.panelGame.Size = new System.Drawing.Size(977, 405);
            this.panelGame.TabIndex = 2;
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(977, 565);
            this.Controls.Add(this.panelGame);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.panelTop);
            this.Name = "GameForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tower Defense Oyunu";
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelBottom.ResumeLayout(false);
            this.panelHUD.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Panel panelGame;
        private System.Windows.Forms.Label lblAltin;
        private System.Windows.Forms.Label lblSkor;
        private System.Windows.Forms.Label lblDalga;
        private System.Windows.Forms.Label lblCan;
        private System.Windows.Forms.Panel panelHUD;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.Button btnYukseltBuyu;
        private System.Windows.Forms.Button btnBuyu;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnYukseltTop;
        private System.Windows.Forms.Button btnYukseltOk;
        private System.Windows.Forms.Button btnTop;
    }
}

