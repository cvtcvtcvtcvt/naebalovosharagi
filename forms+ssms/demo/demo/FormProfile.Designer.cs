namespace demo
{
    partial class FormProfile
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblLogin = new System.Windows.Forms.Label();
            this.lblRole = new System.Windows.Forms.Label();
            this.lblPassHint = new System.Windows.Forms.Label();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // логин пользователя
            this.lblLogin.Text = "Логин: ";
            this.lblLogin.Location = new System.Drawing.Point(50, 20);
            this.lblLogin.Width = 250;

            // роль пользователя
            this.lblRole.Text = "Роль: ";
            this.lblRole.Location = new System.Drawing.Point(50, 40);
            this.lblRole.Width = 250;

            this.lblPassHint.Text = "Новый пароль (макс 3 символа):";
            this.lblPassHint.Location = new System.Drawing.Point(50, 70);
            this.lblPassHint.Width = 250;

            this.txtPass.Location = new System.Drawing.Point(50, 90);
            this.txtPass.Width = 200;
            this.txtPass.PasswordChar = '*';

            // кнопка сохранить пароль
            this.btnSave.Text = "Изменить пароль";
            this.btnSave.Location = new System.Drawing.Point(50, 125);
            this.btnSave.Width = 140;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            // кнопка выйти из аккаунта
            this.btnLogout.Text = "Выйти";
            this.btnLogout.Location = new System.Drawing.Point(50, 160);
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);

            this.ClientSize = new System.Drawing.Size(320, 220);
            this.Text = "Профиль";
            this.Controls.Add(this.lblLogin);
            this.Controls.Add(this.lblRole);
            this.Controls.Add(this.lblPassHint);
            this.Controls.Add(this.txtPass);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnLogout);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Label lblLogin;
        private System.Windows.Forms.Label lblRole;
        private System.Windows.Forms.Label lblPassHint;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnLogout;
    }
}

