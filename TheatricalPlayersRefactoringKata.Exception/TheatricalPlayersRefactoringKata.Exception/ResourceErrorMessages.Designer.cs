﻿//------------------------------------------------------------------------------
// <auto-generated>
//     O código foi gerado por uma ferramenta.
//     Versão de Tempo de Execução:4.0.30319.42000
//
//     As alterações ao arquivo poderão causar comportamento incorreto e serão perdidas se
//     o código for gerado novamente.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TheatricalPlayersRefactoringKata.Exception {
    using System;
    
    
    /// <summary>
    ///   Uma classe de recurso de tipo de alta segurança, para pesquisar cadeias de caracteres localizadas etc.
    /// </summary>
    // Essa classe foi gerada automaticamente pela classe StronglyTypedResourceBuilder
    // através de uma ferramenta como ResGen ou Visual Studio.
    // Para adicionar ou remover um associado, edite o arquivo .ResX e execute ResGen novamente
    // com a opção /str, ou recrie o projeto do VS.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class ResourceErrorMessages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        public ResourceErrorMessages() {
        }
        
        /// <summary>
        ///   Retorna a instância de ResourceManager armazenada em cache usada por essa classe.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("TheatricalPlayersRefactoringKata.Exception.ResourceErrorMessages", typeof(ResourceErrorMessages).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Substitui a propriedade CurrentUICulture do thread atual para todas as
        ///   pesquisas de recursos que usam essa classe de recurso de tipo de alta segurança.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }

        /// <summary>
        ///   Consulta uma cadeia de caracteres localizada semelhante a Audience need to be greather than 0.
        /// </summary>
        public static string AUDIENCE_INVALID {
            get {
                return ResourceManager.GetString("AUDIENCE_INVALID", resourceCulture);
            }
        }

        /// <summary>
        ///   Consulta uma cadeia de caracteres localizada semelhante a Customer name cannot be empty.
        /// </summary>
        public static string CUSTOMER_NAME_INVALID {
            get {
                return ResourceManager.GetString("CUSTOMER_NAME_INVALID", resourceCulture);
            }
        }

        /// <summary>
        ///   Consulta uma cadeia de caracteres localizada semelhante a Invoice not found.
        /// </summary>
        public static string INVOICE_NOT_FOUND {
            get {
                return ResourceManager.GetString("INVOICE_NOT_FOUND", resourceCulture);
            }
        }

        /// <summary>
        ///   Consulta uma cadeia de caracteres localizada semelhante a The lines needs to be greater than 1000 and less than 4000.
        /// </summary>
        public static string LINES_IN_INTERVAL {
            get {
                return ResourceManager.GetString("LINES_IN_INTERVAL", resourceCulture);
            }
        }

        /// <summary>
        ///   Consulta uma cadeia de caracteres localizada semelhante a The performance need to include the play name and the audience need to be greather than 0.
        /// </summary>
        public static string PERFORMANCES_INVALID {
            get {
                return ResourceManager.GetString("PERFORMANCES_INVALID", resourceCulture);
            }
        }

        /// <summary>
        ///   Consulta uma cadeia de caracteres localizada semelhante a Play name cannot be empty.
        /// </summary>
        public static string PLAY_INVALID {
            get {
                return ResourceManager.GetString("PLAY_INVALID", resourceCulture);
            }
        }

        /// <summary>
        ///   Consulta uma cadeia de caracteres localizada semelhante a The play type is invalid.
        /// </summary>
        public static string PLAY_TYPE_INVALID {
            get {
                return ResourceManager.GetString("PLAY_TYPE_INVALID", resourceCulture);
            }
        }

        /// <summary>
        ///   Consulta uma cadeia de caracteres localizada semelhante a Unknown error.
        /// </summary>
        public static string UNKNOWN_ERROR {
            get {
                return ResourceManager.GetString("UNKNOWN_ERROR", resourceCulture);
            }
        }
    }
}
