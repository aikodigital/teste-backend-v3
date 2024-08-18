using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using TheatricalPlayersRefactoringKata.Core.Entities;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Converters
{
    public class GenreConverter : JsonConverter<Genre>
    {
        public override Genre Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var genreString = reader.GetString();
            if (Enum.TryParse<Genre>(genreString, ignoreCase: true, out var genre))
            {
                return genre;
            }
            throw new JsonException("Invalid genre value.");
        }

        public override void Write(Utf8JsonWriter writer, Genre value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}