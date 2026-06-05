namespace demo
{
    partial class FormAuth
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
            this.lblPass = new System.Windows.Forms.Label();
            this.txtLogin = new System.Windows.Forms.TextBox();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.btnVhod = new System.Windows.Forms.Button();
            this.btnReg = new System.Windows.Forms.Button();
            this.btnGuest = new System.Windows.Forms.Button();
            this.SuspendLayout();

            this.lblLogin.Text = "Логин:";
            this.lblLogin.Location = new System.Drawing.Point(50, 12);
            this.lblLogin.Width = 200;

            this.txtLogin.Location = new System.Drawing.Point(50, 30);
            this.txtLogin.Width = 200;

            this.lblPass.Text = "Пароль (макс 3 символа):";
            this.lblPass.Location = new System.Drawing.Point(50, 58);
            this.lblPass.Width = 200;

            this.txtPass.Location = new System.Drawing.Point(50, 75);
            this.txtPass.Width = 200;
            this.txtPass.PasswordChar = '*';

            // кнопка войти
            this.btnVhod.Text = "Войти";
            this.btnVhod.Location = new System.Drawing.Point(50, 115);
            this.btnVhod.Click += new System.EventHandler(this.btnVhod_Click);

            // кнопка регистрация
            this.btnReg.Text = "Зарегистрироваться";
            this.btnReg.Location = new System.Drawing.Point(150, 115);
            this.btnReg.Width = 150;
            this.btnReg.Click += new System.EventHandler(this.btnReg_Click);

            // кнопка войти как гость
            this.btnGuest.Text = "Войти как гость";
            this.btnGuest.Location = new System.Drawing.Point(50, 150);
            this.btnGuest.Width = 250;
            this.btnGuest.Click += new System.EventHandler(this.btnGuest_Click);

            this.ClientSize = new System.Drawing.Size(360, 210);
            this.Text = "Вход / Регистрация";
            this.Controls.Add(this.lblLogin);
            this.Controls.Add(this.lblPass);
            this.Controls.Add(this.txtLogin);
            this.Controls.Add(this.txtPass);
            this.Controls.Add(this.btnVhod);
            this.Controls.Add(this.btnReg);
            this.Controls.Add(this.btnGuest);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Label lblLogin;
        private System.Windows.Forms.Label lblPass;
        private System.Windows.Forms.TextBox txtLogin;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.Button btnVhod;
        private System.Windows.Forms.Button btnReg;
        private System.Windows.Forms.Button btnGuest;
    }
}
