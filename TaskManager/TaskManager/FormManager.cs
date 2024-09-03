using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TaskManager
{
    public static class FormManager
    {
        private static Dictionary<string, Form> openForms = new Dictionary<string, Form>();

        public static void ShowForm<T>(string formKey) where T : Form, new()
        {
            // Close all other forms
            CloseAllForms();

            // Check if form is already open
            if (openForms.ContainsKey(formKey))
            {
                openForms[formKey].BringToFront();
                openForms[formKey].Show();
                return;
            }

            // Create and show new form
            T form = new T();
            openForms[formKey] = form;
            form.FormClosed += (sender, e) => openForms.Remove(formKey);
            form.Show();
        }

        public static void CloseAllForms()
        {
            var formsToClose = new List<Form>(openForms.Values);
            foreach (var form in formsToClose)
            {
                if (!form.IsDisposed)
                {
                    form.Close();
                }
            }
            openForms.Clear();
        }

        public static void CloseForm(string formKey)
        {
            if (openForms.ContainsKey(formKey))
            {
                openForms[formKey].Close();
            }
        }
    }
}

