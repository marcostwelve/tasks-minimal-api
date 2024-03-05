using Dapper.Contrib.Extensions;
using TarefasAPI.Data;
using static TarefasAPI.Data.TarefaContext;

namespace TarefasAPI.EndPoints
{
    public static class TarefasEndpoints
    {
        public static void MapTarefasEndpoints(this WebApplication app)
        {
            app.MapGet("/", () => $"Bem vindo a API Tarefas - {DateTime.Now}");

            app.MapGet("/tarefas", async (GetConnection connectionGetter) =>
            {
                using var context = await connectionGetter();
                var tarefas = context.GetAll<Tarefa>().ToList();

                if(tarefas is null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(tarefas);

            });

            app.MapGet("/tarefas/{id}", async (GetConnection connectionGetter, int id) =>
            {
                using var context = await connectionGetter();
                return context.Get<Tarefa>(id) is Tarefa tarefa ? Results.Ok(tarefa) : Results.NotFound();

                //var tarefa = context.Get<Tarefa>(id);
                //if (tarefa is null)
                //{
                //    return Results.NotFound();
                //}
                //return Results.Ok(tarefa);

            });

            app.MapPost("/tarefas", async (GetConnection connectionGetter, Tarefa tarefa) =>
            {
                using ( var context = await connectionGetter())
                {
                    var tarefaCriada = context.Insert(tarefa);
                    return Results.Created($"/tarefas/{tarefa.Id}", tarefaCriada);
                }
            });

            app.MapPut("/tarefas/{id}", async (GetConnection connectionGetter, int id, Tarefa tarefa) =>
            {
                using(var context = await connectionGetter())
                {
                    var tarefaAtualizada = context.Update(tarefa);
                    return Results.Ok();
                }
            });

            app.MapDelete("/tarefas/{id}", async (GetConnection connectionGetter, int id) =>
            {
                var context = await connectionGetter();
                var tarefa = context.Get<Tarefa>(id);

                if(tarefa is null)
                {
                    return Results.NotFound();
                }

                context.Delete(tarefa);
                return Results.Ok(tarefa);
            });
        }
    }
}
