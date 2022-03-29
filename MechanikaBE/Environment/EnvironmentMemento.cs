using System;
using System.Collections.Generic;
using System.Text;

namespace Mechanika
{
    public partial class Environment : IMemorable
    {
        public IMemento GetMemento(string operationName)
        {
            return new EnvironmentMemento(this, operationName);
        }
        public void RestoreFrom(IMemento memento)
        {
            if (!(memento is EnvironmentMemento)) return;
            var envMemento = memento as EnvironmentMemento;
            belki = envMemento.belki;
            obciazenia = envMemento.obciazenia;
            podpory = envMemento.podpory;
            przeguby = envMemento.przeguby;
            liczbaObcPodp = envMemento.liczbaObcPod;
        }
        class EnvironmentMemento : IMemento
        {
            public List<Belka> belki = new List<Belka>();
            public List<Obciazenie> obciazenia = new List<Obciazenie>();
            public List<Podpora> podpory = new List<Podpora>();
            public List<Przegub> przeguby = new List<Przegub>();

            public int liczbaObcPod;

            string opName;

            public EnvironmentMemento(Environment env, string opName)
            {
                liczbaObcPod = env.liczbaObcPodp;
                belki = new List<Belka>();
                foreach (Belka b in env.belki)
                    belki.Add(b.Clone());
                obciazenia = new List<Obciazenie>();
                foreach (Obciazenie o in env.obciazenia)
                    obciazenia.Add(o.Clone());
                podpory = new List<Podpora>();
                foreach (Podpora p in env.podpory)
                    podpory.Add(p.Clone());
                przeguby = new List<Przegub>();
                foreach (Przegub p in env.przeguby)
                    przeguby.Add(p.Clone(belki));

                this.opName = opName;
            }
            public string GetOperationName() => opName;
        }
    }
}
