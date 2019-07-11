namespace WindowsFormsApplication1
{
    partial class FormWordGame
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBoxDialog = new System.Windows.Forms.TextBox();
            this.listViewPlayers = new System.Windows.Forms.ListView();
            this.labelError = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBoxDialog
            // 
            this.textBoxDialog.Location = new System.Drawing.Point(12, 55);
            this.textBoxDialog.Multiline = true;
            this.textBoxDialog.Name = "textBoxDialog";
            this.textBoxDialog.Size = new System.Drawing.Size(391, 419);
            this.textBoxDialog.TabIndex = 0;
            // 
            // listViewPlayers
            // 
            this.listViewPlayers.Location = new System.Drawing.Point(603, 55);
            this.listViewPlayers.Name = "listViewPlayers";
            this.listViewPlayers.Size = new System.Drawing.Size(276, 419);
            this.listViewPlayers.TabIndex = 1;
            this.listViewPlayers.UseCompatibleStateImageBehavior = false;
            // 
            // labelError
            // 
            this.labelError.AutoSize = true;
            this.labelError.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelError.Location = new System.Drawing.Point(471, 454);
            this.labelError.Name = "labelError";
            this.labelError.Size = new System.Drawing.Size(57, 20);
            this.labelError.TabIndex = 2;
            this.labelError.Text = "label1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(125, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Диалог с игроком  ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(720, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Список игроков";
            // 
            // FormWordGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(891, 486);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelError);
            this.Controls.Add(this.listViewPlayers);
            this.Controls.Add(this.textBoxDialog);
            this.Name = "FormWordGame";
            this.Text = "FormWordGame";
            this.Load += new System.EventHandler(this.FormWordGame_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxDialog;
        private System.Windows.Forms.ListView listViewPlayers;
        private System.Windows.Forms.Label labelError;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}