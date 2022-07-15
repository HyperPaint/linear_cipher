using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace LinearCypher
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private const string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; //"АБВГДЕЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";

        private bool check()
        {
            bool ok = true;
            if (!check_number(key_a.Text))
            {
                MessageBox.Show("Часть ключа А содержит недопустимые символы");
                ok = false;
            }
            if (!check_number(key_b.Text))
            {
                MessageBox.Show("Часть ключа B содержит недопустимые символы");
                ok = false;
            }
            if (!check_alphabet(text.Text))
            {
                MessageBox.Show("Текст содержит недопустимые символы");
                ok = false;
            }
            return ok;
        }

        private bool check_number(string str)
        {
            try
            {
                Convert.ToInt32(str);
            }
            catch
            {
                return false;
            }
            return true;
        }

        private bool check_alphabet(string str)
        {
            foreach (var item in str)
            {
                if ( !alphabet.Contains(item) )
                {
                    return false;
                }
            }
            return true;
        }

        private string encrypt_text(string text, int a, int b)
        {
            int result, y;
            string result_text = "";
            for (int i = 0; i < text.Length; i++)
            {
                y = (text[i] - alphabet[0]);
                result = (a * y + b) % alphabet.Length;
                result_text += alphabet[result];
            }
            return result_text;
        }

        private string decrypt_text(string text, int a, int b)
        {
            if (a % 2 == 0)
            {
                MessageBox.Show("Часть ключа А содержит недопустимые символы");
                return text;
            }

            int a_ = 0;
            while ((a * a_) % alphabet.Length != 1)
            {
                a_++;
            }

            int result, y;
            string result_text = "";
            for (int i = 0; i < text.Length; i++)
            {
                y = (text[i] - alphabet[0]);
                result = a_ * (y - b);
                while (result < 0)
                {
                    result += alphabet.Length;
                }
                result = result % alphabet.Length;
                result_text += alphabet[result];
            }
            return result_text;
        }

        private void encrypt_Click(object sender, EventArgs e)
        {
            if (check())
            {
                text.Text = encrypt_text(text.Text, Convert.ToInt32(key_a.Text), Convert.ToInt32(key_b.Text));
                File.WriteAllText("text.txt", text.Text);
            }
        }

        private void decrypt_Click(object sender, EventArgs e)
        {
            if (check())
            {
                text.Text = decrypt_text(text.Text, Convert.ToInt32(key_a.Text), Convert.ToInt32(key_b.Text));
                File.WriteAllText("text.txt", text.Text);
            }
        }
    }
}
