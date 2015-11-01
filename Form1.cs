using Autofac;
using Autofac.Core;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AutofacDemoWin
{
    public partial class FormAutofacDemoWin : Form
    {
        private IContainer _container;

        public FormAutofacDemoWin()
        {
            InitializeComponent();
        }

        private void FormAutofacDemoWin_Load(object sender, EventArgs e)
        {
            var builder = new ContainerBuilder();
            /*
            NamedPropertyParameter paramLinguagemPrincipal = new NamedPropertyParameter("LinguagemProgramacao", new LinguagemCSharp());
            NamedPropertyParameter paramLinguagemSecundaria = new NamedPropertyParameter("LinguagemProgramacaoSecundaria", new LinguagemGO());

            builder.RegisterType<DesenvolvedorNinja>().As<IDesenvolvedor>().WithProperties(new List<Parameter>() { paramLinguagemPrincipal, paramLinguagemSecundaria }).InstancePerDependency();
            */
            builder.Register(c => new DesenvolvedorNinja { LinguagemProgramacao = c.Resolve<ILinguagemProgramacao>(), LinguagemProgramacaoSecundaria = new LinguagemGO() }).As<IDesenvolvedor>().InstancePerDependency();

            builder.RegisterType<LinguagemCSharp>().As<ILinguagemProgramacao>().AsSelf().InstancePerDependency();
            builder.RegisterType<LinguagemGO>().AsSelf().InstancePerDependency();
            _container = builder.Build();
        }

        private void buttonObterLinguagemAutofac_Click(object sender, EventArgs e)
        {
            IDesenvolvedor desenvolvedor = _container.Resolve<IDesenvolvedor>();
            MessageBox.Show(desenvolvedor.GetLinguagemProgramacao());
        }
    }
}
