using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata.General;

public class Utf8StringWriter : StringWriter
{
	public override Encoding Encoding => Encoding.UTF8;
}
