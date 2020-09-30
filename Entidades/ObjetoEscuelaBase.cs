using System;

namespace Etapa5.Entidades 
{
    //ABSTRACT => Esta clase puede tener clases hijas, más no se puede instanciar o crear objetos
    public abstract class ObjetoEscuelaBase {
        public string UniqueId { get; private set; }
        public string Nombre { get; set; }
        public ObjetoEscuelaBase() {
            UniqueId = Guid.NewGuid().ToString();
        }

        public override string ToString()
        {
            return $"{Nombre}, {UniqueId}";
        }

    }
}