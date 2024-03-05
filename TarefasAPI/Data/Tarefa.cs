using System.ComponentModel.DataAnnotations.Schema;

namespace TarefasAPI.Data;

[Table("Tarefas")]
public record Tarefa(int Id, string Atividade, string Status);
