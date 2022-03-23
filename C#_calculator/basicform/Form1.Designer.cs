namespace basicform
{
    partial class Form1
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
            this.zero = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.one = new System.Windows.Forms.Button();
            this.two = new System.Windows.Forms.Button();
            this.three = new System.Windows.Forms.Button();
            this.five = new System.Windows.Forms.Button();
            this.six = new System.Windows.Forms.Button();
            this.seven = new System.Windows.Forms.Button();
            this.eight = new System.Windows.Forms.Button();
            this.nine = new System.Windows.Forms.Button();
            this.four = new System.Windows.Forms.Button();
            this.mod = new System.Windows.Forms.Button();
            this.period = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.plus = new System.Windows.Forms.Button();
            this.minus = new System.Windows.Forms.Button();
            this.times = new System.Windows.Forms.Button();
            this.divide = new System.Windows.Forms.Button();
            this.enter = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.clear = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // zero
            // 
            this.zero.Font = new System.Drawing.Font("Comic Sans MS", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.zero.Location = new System.Drawing.Point(35, 435);
            this.zero.Name = "zero";
            this.zero.Size = new System.Drawing.Size(77, 37);
            this.zero.TabIndex = 0;
            this.zero.Text = "0";
            this.zero.UseVisualStyleBackColor = true;
            this.zero.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("DejaVu Sans Mono", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(35, 50);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(478, 54);
            this.textBox1.TabIndex = 10;
            this.textBox1.Text = "2";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CheckEnterKeyPress);
            // 
            // one
            // 
            this.one.Font = new System.Drawing.Font("Comic Sans MS", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.one.Location = new System.Drawing.Point(35, 367);
            this.one.Name = "one";
            this.one.Size = new System.Drawing.Size(77, 37);
            this.one.TabIndex = 11;
            this.one.Text = "1";
            this.one.UseVisualStyleBackColor = true;
            this.one.Click += new System.EventHandler(this.button2_Click);
            // 
            // two
            // 
            this.two.Font = new System.Drawing.Font("Comic Sans MS", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.two.Location = new System.Drawing.Point(159, 367);
            this.two.Name = "two";
            this.two.Size = new System.Drawing.Size(77, 37);
            this.two.TabIndex = 12;
            this.two.Text = "2";
            this.two.UseVisualStyleBackColor = true;
            this.two.Click += new System.EventHandler(this.button3_Click);
            // 
            // three
            // 
            this.three.Font = new System.Drawing.Font("Comic Sans MS", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.three.Location = new System.Drawing.Point(281, 367);
            this.three.Name = "three";
            this.three.Size = new System.Drawing.Size(77, 37);
            this.three.TabIndex = 13;
            this.three.Text = "3";
            this.three.UseVisualStyleBackColor = true;
            this.three.Click += new System.EventHandler(this.three_Click);
            // 
            // five
            // 
            this.five.Font = new System.Drawing.Font("Comic Sans MS", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.five.Location = new System.Drawing.Point(159, 298);
            this.five.Name = "five";
            this.five.Size = new System.Drawing.Size(77, 37);
            this.five.TabIndex = 14;
            this.five.Text = "5";
            this.five.UseVisualStyleBackColor = true;
            this.five.Click += new System.EventHandler(this.five_Click);
            // 
            // six
            // 
            this.six.Font = new System.Drawing.Font("Comic Sans MS", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.six.Location = new System.Drawing.Point(281, 298);
            this.six.Name = "six";
            this.six.Size = new System.Drawing.Size(77, 37);
            this.six.TabIndex = 15;
            this.six.Text = "6";
            this.six.UseVisualStyleBackColor = true;
            this.six.Click += new System.EventHandler(this.six_Click);
            // 
            // seven
            // 
            this.seven.Font = new System.Drawing.Font("Comic Sans MS", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.seven.Location = new System.Drawing.Point(35, 233);
            this.seven.Name = "seven";
            this.seven.Size = new System.Drawing.Size(77, 37);
            this.seven.TabIndex = 16;
            this.seven.Text = "7";
            this.seven.UseVisualStyleBackColor = true;
            this.seven.Click += new System.EventHandler(this.seven_Click);
            // 
            // eight
            // 
            this.eight.Font = new System.Drawing.Font("Comic Sans MS", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.eight.Location = new System.Drawing.Point(159, 233);
            this.eight.Name = "eight";
            this.eight.Size = new System.Drawing.Size(77, 37);
            this.eight.TabIndex = 17;
            this.eight.Text = "8";
            this.eight.UseVisualStyleBackColor = true;
            this.eight.Click += new System.EventHandler(this.eight_Click);
            // 
            // nine
            // 
            this.nine.Font = new System.Drawing.Font("Comic Sans MS", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nine.Location = new System.Drawing.Point(281, 233);
            this.nine.Name = "nine";
            this.nine.Size = new System.Drawing.Size(77, 37);
            this.nine.TabIndex = 18;
            this.nine.Text = "9";
            this.nine.UseVisualStyleBackColor = true;
            this.nine.Click += new System.EventHandler(this.nine_Click);
            // 
            // four
            // 
            this.four.Font = new System.Drawing.Font("Comic Sans MS", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.four.Location = new System.Drawing.Point(35, 298);
            this.four.Name = "four";
            this.four.Size = new System.Drawing.Size(77, 37);
            this.four.TabIndex = 19;
            this.four.Text = "4";
            this.four.UseVisualStyleBackColor = true;
            this.four.Click += new System.EventHandler(this.four_Click);
            // 
            // mod
            // 
            this.mod.Font = new System.Drawing.Font("Comic Sans MS", 10F);
            this.mod.Location = new System.Drawing.Point(430, 453);
            this.mod.Name = "mod";
            this.mod.Size = new System.Drawing.Size(122, 36);
            this.mod.TabIndex = 20;
            this.mod.Text = "%";
            this.mod.UseVisualStyleBackColor = true;
            this.mod.Click += new System.EventHandler(this.mod_Click);
            // 
            // period
            // 
            this.period.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.period.Location = new System.Drawing.Point(159, 431);
            this.period.Name = "period";
            this.period.Size = new System.Drawing.Size(77, 41);
            this.period.TabIndex = 21;
            this.period.Text = ".";
            this.period.UseVisualStyleBackColor = true;
            this.period.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // textBox2
            // 
            this.textBox2.AllowDrop = true;
            this.textBox2.Font = new System.Drawing.Font("Courier New", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.Location = new System.Drawing.Point(35, 132);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox2.Size = new System.Drawing.Size(478, 69);
            this.textBox2.TabIndex = 22;
            this.textBox2.Text = "(800) * (1.5) / (-8) + (50) - (130) % ";
            // 
            // plus
            // 
            this.plus.Font = new System.Drawing.Font("Comic Sans MS", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.plus.Location = new System.Drawing.Point(430, 401);
            this.plus.Name = "plus";
            this.plus.Size = new System.Drawing.Size(122, 37);
            this.plus.TabIndex = 23;
            this.plus.Text = "+";
            this.plus.UseVisualStyleBackColor = true;
            this.plus.Click += new System.EventHandler(this.plus_Click);
            // 
            // minus
            // 
            this.minus.Font = new System.Drawing.Font("Comic Sans MS", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.minus.Location = new System.Drawing.Point(430, 348);
            this.minus.Name = "minus";
            this.minus.Size = new System.Drawing.Size(122, 37);
            this.minus.TabIndex = 24;
            this.minus.Text = "-";
            this.minus.UseVisualStyleBackColor = true;
            this.minus.Click += new System.EventHandler(this.minus_Click);
            // 
            // times
            // 
            this.times.Font = new System.Drawing.Font("Comic Sans MS", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.times.Location = new System.Drawing.Point(430, 298);
            this.times.Name = "times";
            this.times.Size = new System.Drawing.Size(122, 37);
            this.times.TabIndex = 25;
            this.times.Text = "*";
            this.times.UseVisualStyleBackColor = true;
            this.times.Click += new System.EventHandler(this.times_Click);
            // 
            // divide
            // 
            this.divide.Font = new System.Drawing.Font("Comic Sans MS", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.divide.Location = new System.Drawing.Point(430, 246);
            this.divide.Name = "divide";
            this.divide.Size = new System.Drawing.Size(122, 37);
            this.divide.TabIndex = 26;
            this.divide.Text = "/";
            this.divide.UseVisualStyleBackColor = true;
            this.divide.Click += new System.EventHandler(this.divide_Click);
            // 
            // enter
            // 
            this.enter.Font = new System.Drawing.Font("Comic Sans MS", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.enter.Location = new System.Drawing.Point(281, 431);
            this.enter.Name = "enter";
            this.enter.Size = new System.Drawing.Size(110, 41);
            this.enter.TabIndex = 27;
            this.enter.Text = "Enter";
            this.enter.UseVisualStyleBackColor = true;
            this.enter.Click += new System.EventHandler(this.button6_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 109);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(329, 20);
            this.label1.TabIndex = 30;
            this.label1.Text = "Press enter to get the output of this equation!";
            // 
            // clear
            // 
            this.clear.BackColor = System.Drawing.Color.Crimson;
            this.clear.ForeColor = System.Drawing.Color.BlanchedAlmond;
            this.clear.Location = new System.Drawing.Point(430, 208);
            this.clear.Name = "clear";
            this.clear.Size = new System.Drawing.Size(122, 32);
            this.clear.TabIndex = 31;
            this.clear.Text = "Clear";
            this.clear.UseVisualStyleBackColor = false;
            this.clear.Click += new System.EventHandler(this.clear_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(596, 508);
            this.Controls.Add(this.clear);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.enter);
            this.Controls.Add(this.divide);
            this.Controls.Add(this.times);
            this.Controls.Add(this.minus);
            this.Controls.Add(this.plus);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.period);
            this.Controls.Add(this.mod);
            this.Controls.Add(this.four);
            this.Controls.Add(this.nine);
            this.Controls.Add(this.eight);
            this.Controls.Add(this.seven);
            this.Controls.Add(this.six);
            this.Controls.Add(this.five);
            this.Controls.Add(this.three);
            this.Controls.Add(this.two);
            this.Controls.Add(this.one);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.zero);
            this.Name = "Form1";
            this.Text = "Calculator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button zero;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button one;
        private System.Windows.Forms.Button two;
        private System.Windows.Forms.Button three;
        private System.Windows.Forms.Button five;
        private System.Windows.Forms.Button six;
        private System.Windows.Forms.Button seven;
        private System.Windows.Forms.Button eight;
        private System.Windows.Forms.Button nine;
        private System.Windows.Forms.Button four;
        private System.Windows.Forms.Button mod;
        private System.Windows.Forms.Button period;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button plus;
        private System.Windows.Forms.Button minus;
        private System.Windows.Forms.Button times;
        private System.Windows.Forms.Button divide;
        private System.Windows.Forms.Button enter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button clear;
    }
}

