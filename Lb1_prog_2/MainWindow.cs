using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ClassHero;
using Microsoft.VisualBasic;

namespace Lb1_prog_2
{
    public partial class MainWindow : Form
    {
        readonly List<Hero> heroes = new();

        public MainWindow()
        {
            InitializeComponent();
            heroes.Add(new Hero("Hero1", 100, 1000, 220, 30));
            heroes.Add(new Hero("Hero2", 100, 1000, 220, 30));
        }


        /// <summary>
        /// Создание панели вывода характеристик героя по его данным
        /// </summary>
        /// <param name="hero">Герой данные которого будут выведены</param>
        /// <param name="nameNum">Номер героя в списке(используется для быстрого нахождения панели нужного героя)</param>
        /// <returns>Возвращает объект панели с полями заполнеными характеристиками данного героя</returns>
        private Panel CreateHeroPanel(Hero hero, int nameNum)
        {
            // Панель контейнер
            Panel heroPanel = new();
            heroPanel.Name = nameNum.ToString();
            heroPanel.Width = 300;
            heroPanel.Height = 400;
            heroPanel.BorderStyle = BorderStyle.FixedSingle;

            // Ярлык имени героя
            Label heroNameLable = new();
            heroNameLable.Text = hero.GetName();
            heroNameLable.Name = "name";
            heroNameLable.Location = new Point(20, 20);
            heroPanel.Controls.Add(heroNameLable);

            // Ярлык с количеством здоровья героя
            Label healthLable = new();
            healthLable.Text = "Здоровье: ";
            healthLable.AutoSize = true;
            healthLable.Location = new Point(20, 60);
            heroPanel.Controls.Add(healthLable);
            Label healthValLable = new();
            healthValLable.Text = hero.GetHealth().ToString();
            healthValLable.Name = "health";
            healthValLable.Location = new Point(20 + healthLable.Width, healthLable.Location.Y);
            heroPanel.Controls.Add(healthValLable);
            // Кнопка для пополнения количества жизней
            Button healButton = new();
            healButton.Text = "HealHero";
            healButton.AutoSize = true;
            healButton.Click += new System.EventHandler(this.HealButton_Click);
            heroPanel.Controls.Add(healButton);
            healButton.Location = new Point(heroPanel.Width - healButton.Width - 20, healthLable.Location.Y);

            // Ярлык с количетвом баллов
            Label scoreLable = new();
            scoreLable.Text = "Баллы: ";
            scoreLable.AutoSize = true;
            scoreLable.Location = new Point(20, 100);
            heroPanel.Controls.Add(scoreLable);
            Label scoreValLabel = new();
            scoreValLabel.Text = hero.GetScore().ToString();
            scoreValLabel.Name = "score";
            scoreValLabel.Location = new Point(20 + scoreLable.Width, scoreLable.Location.Y);
            heroPanel.Controls.Add(scoreValLabel);
            // Кнопка перевода бонусов в баллы
            Button topUpScoreButton = new();
            topUpScoreButton.Text = "TopUpScore";
            topUpScoreButton.AutoSize = true;
            topUpScoreButton.Click += new System.EventHandler(this.TopUpScoreButton_Click);
            heroPanel.Controls.Add(topUpScoreButton);
            topUpScoreButton.Location = new Point(heroPanel.Width - topUpScoreButton.Width - 20, scoreLable.Location.Y);

            // Ярлык с количеством бонусов
            Label bonusesLable = new();
            bonusesLable.Text = "Бонусы: ";
            bonusesLable.AutoSize = true;
            bonusesLable.Location = new Point(20, 140);
            heroPanel.Controls.Add(bonusesLable);
            Label bonusesValLabel = new();
            bonusesValLabel.Text = hero.GetBonuses().ToString();
            bonusesValLabel.Name = "bonuses";
            bonusesValLabel.Location = new Point(20 + bonusesLable.Width, bonusesLable.Location.Y);
            heroPanel.Controls.Add(bonusesValLabel);

            // Ярлык уровня героя
            Label levelLable = new();
            levelLable.Text = "Уровень: ";
            levelLable.AutoSize = true;
            levelLable.Location = new Point(20, 180);
            heroPanel.Controls.Add(levelLable);
            Label levelValLable = new();
            levelValLable.Text = hero.GetLevel().ToString();
            levelValLable.Name = "level";
            levelValLable.Location = new Point(20 + levelLable.Width, levelLable.Location.Y);
            heroPanel.Controls.Add(levelValLable);
            // Кнопка увеличения уровня
            Button increaseLevelButton = new();
            increaseLevelButton.Text = "IncreaseLevel";
            increaseLevelButton.AutoSize = true;
            increaseLevelButton.Click += new System.EventHandler(this.IncreaseLevelButton_Click);
            heroPanel.Controls.Add(increaseLevelButton);
            increaseLevelButton.Location = new Point(heroPanel.Width - increaseLevelButton.Width - 20, levelLable.Location.Y);

            // Выбор оппонента для боя
            ComboBox opponentComboBox = new();
            opponentComboBox.Location = new Point(20, 220);
            foreach (Hero el in heroes)
                opponentComboBox.Items.Add(el.GetName());
            opponentComboBox.AutoSize = true;
            opponentComboBox.Name = "opponent";
            opponentComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            opponentComboBox.SelectedIndexChanged += new EventHandler(this.OpponentComboBox_SelectedIndexChanged);
            heroPanel.Controls.Add(opponentComboBox);

            // Кнопка начала боя
            Button fightButton = new();
            fightButton.Text = "fight";
            fightButton.Enabled = false;
            fightButton.Name = "fight";
            fightButton.Click += new EventHandler(this.FightButton_Click);
            heroPanel.Controls.Add(fightButton);
            fightButton.Location = new Point(heroPanel.Width - fightButton.Width - 20, 220);

            return heroPanel;
        }

        /// <summary>
        /// Очистка главного экрана и добавление полей для ввода данных о новом герое
        /// </summary>
        private void AddHeroPanel()
        {
            mainPanel.Controls.Clear();
            // Поле ввода имени нового героя
            TextBox nameTextBox = new();
            nameTextBox.PlaceholderText = "Имя";
            nameTextBox.Name = "name";
            nameTextBox.Location = new Point(20, 20);
            mainPanel.Controls.Add(nameTextBox);

            // Поле ввода жизней нового героя
            TextBox healthTextBox = new();
            healthTextBox.PlaceholderText = "Здоровье";
            healthTextBox.Name = "health";
            healthTextBox.Location = new Point(20, 60);
            mainPanel.Controls.Add(healthTextBox);

            // Поле ввода баллов нового героя
            TextBox scoreTextBox = new();
            scoreTextBox.PlaceholderText = "Баллы";
            scoreTextBox.Name = "score";
            scoreTextBox.Location = new Point(20, 100);
            mainPanel.Controls.Add(scoreTextBox);

            // Поле ввода бонусов нового героя
            TextBox bonusesTextBox = new();
            bonusesTextBox.PlaceholderText = "Бонусы";
            bonusesTextBox.Name = "bonuses";
            bonusesTextBox.Location = new Point(20, 140);
            mainPanel.Controls.Add(bonusesTextBox);

            // Поле ввода уровня нового героя
            TextBox levelTextBox = new();
            levelTextBox.PlaceholderText = "Уровень";
            levelTextBox.Name = "level";
            levelTextBox.Location = new Point(20, 180);
            mainPanel.Controls.Add(levelTextBox);

            // Кнопка подтверждения добавления нового героя
            Button confirmButton = new();
            confirmButton.Text = "Подтвердить";
            confirmButton.Click += new EventHandler(this.ConfirmButton_Click);
            mainPanel.Controls.Add(confirmButton);
        }


        /// <summary>
        /// Добавление панелей с характеристиками героев на главное окно
        /// </summary>
        private void DrowAll()
        {
            for (int i = 0; i < heroes.Count; i++)
                mainPanel.Controls.Add(CreateHeroPanel(heroes[i], i));
        }

        /// <summary>
        /// Обновление всех полей со значениями характеристик героев и проверка возможности начала боя с выбранным оппонентом
        /// </summary>
        private void RefreshAll()
        {
            for(int i = 0; i < heroes.Count; i++)
            {
                // Перезапись значений
                Panel panel = mainPanel.Controls[i.ToString()] as Panel;
                (panel.Controls["health"] as Label).Text = heroes[i].GetHealth().ToString();
                (panel.Controls["score"] as Label).Text = heroes[i].GetScore().ToString();
                (panel.Controls["bonuses"] as Label).Text = heroes[i].GetBonuses().ToString();
                (panel.Controls["level"] as Label).Text = heroes[i].GetLevel().ToString();

                // Проверка возможности проведения боя (выбран ли кто либо, не выбран ли сам герой, достаточно ли здоровья у герое для боя)
                panel.Controls["fight"].Enabled = false;
                if ((panel.Controls["opponent"] as ComboBox).SelectedItem == null) continue;
                Hero opponent = heroes.Find(e => e.GetName() == (panel.Controls["opponent"] as ComboBox).SelectedItem.ToString());
                if (opponent.GetHealth() > 0 && opponent.GetName() != heroes[i].GetName() && heroes[i].GetHealth() > 0)
                    panel.Controls["fight"].Enabled = true;
            }
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            DrowAll();
        }

        /// <summary>
        /// Пополнение жизней на уазанное количество
        /// </summary>
        /// <param name="sender">Объект кнопки</param>
        /// <param name="e">Подробности события</param>
        private void HealButton_Click(object sender, EventArgs e)
        {
            int heroNum = int.Parse((sender as Button).Parent.Name);
            // Вывод окна для ввода количества жизней
            int addHealth = int.Parse(Interaction.InputBox("Введите количество жизней:", "Пополнить жизни"));
            heroes[heroNum].HealHero(addHealth);
            RefreshAll();
        }

        /// <summary>
        /// Перевод бонусов в баллы
        /// </summary>
        /// <param name="sender">Объект кнопки</param>
        /// <param name="e">Подробности события</param>
        private void TopUpScoreButton_Click(object sender, EventArgs e)
        {
            int heroNum = int.Parse((sender as Button).Parent.Name);
            // Вывод окна для ввода количества бонусов
            int addScore = int.Parse(Interaction.InputBox("Введите количество бонусов:", "Пополнить баллы"));
            if (!heroes[heroNum].TopUpScore(addScore))
                MessageBox.Show("Нельзя провести перевод!", "Ошибка перевода");
            RefreshAll();
        }

        /// <summary>
        /// Увеличение уровня на 1
        /// </summary>
        /// <param name="sender">Объект кнопки</param>
        /// <param name="e">Подробности события</param>
        private void IncreaseLevelButton_Click(object sender, EventArgs e)
        {
            int heroNum = int.Parse((sender as Button).Parent.Name);
            heroes[heroNum].IncreaseLevel();
            RefreshAll();
        }

        /// <summary>
        /// Запуск боя с оппонентом
        /// </summary>
        /// <param name="sender">Объект кнопки</param>
        /// <param name="e">Подробности события</param>
        private void FightButton_Click(object sender, EventArgs e)
        {
            int heroNum = int.Parse((sender as Button).Parent.Name);
            string opponentName = ((sender as Button).Parent.Controls["opponent"] as ComboBox).SelectedItem.ToString();
            Hero opponent = heroes.Find(e => e.GetName() == opponentName);
            string winnerName;
            if (heroes[heroNum].FightWith(opponent))
                winnerName = heroes[heroNum].GetName();
            else
                winnerName = opponent.GetName();
            MessageBox.Show("Победил "+ winnerName, "Результат битвы");
            RefreshAll();
        }

        /// <summary>
        /// Обновление содержимого при смене оппонента
        /// </summary>
        /// <param name="sender">Объект кнопки</param>
        /// <param name="e">Подробности события</param>
        private void OpponentComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshAll();
        }

        /// <summary>
        /// Вывод полей для добавления нового героя
        /// </summary>
        /// <param name="sender">Объект кнопки</param>
        /// <param name="e">Подробности события</param>
        private void AddHeroButton_Click(object sender, EventArgs e)
        {
            AddHeroPanel();
        }

        /// <summary>
        /// Добавления героя в список и вывод характеристик героев
        /// </summary>
        /// <param name="sender">Объект кнопки</param>
        /// <param name="e">Подробности события</param>
        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            Hero hero = new((mainPanel.Controls["name"] as TextBox).Text,
                int.Parse((mainPanel.Controls["health"] as TextBox).Text),
                int.Parse((mainPanel.Controls["score"] as TextBox).Text),
                int.Parse((mainPanel.Controls["bonuses"] as TextBox).Text),
                int.Parse((mainPanel.Controls["level"] as TextBox).Text));
            heroes.Add(hero);
            mainPanel.Controls.Clear();
            DrowAll();
        }
    }
}
