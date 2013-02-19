/// <reference path="jquery-1.7.1.js" />
/// <reference path="knockout-2.1.0.debug.js" />
/// <reference path="json2.js" />
/// <reference path="knockout-2.1.0.js" />
var todo = todo || {};

todo.url = '/api/tarefas';

todo.Tarefa = function () {
    var id = ko.observable(0);
    var terminada = ko.observable(false);
    var titulo = ko.observable('');
    return {
        id: id,
        terminada: terminada,
        titulo: titulo
    };
};

todo.salvar = function (tarefa) {
    $.post(this.url, ko.toJS(tarefa), function (data) { tarefa.id(data.Id); }, 'json');
};

todo.atualizar = function(tarefa) {
    $.ajax({ url: this.url + '/' + tarefa.id(), type: 'PUT', data: ko.toJS(tarefa) });
};

todo.recuperar = function() {
     $.getJSON(todo.url, function(data) {
         $(data).each(function () {
            var tarefa = todo.viewmodel.criarTarefa(this);
            todo.viewmodel.tarefas.push(tarefa);
        });
    });
};

todo.viewmodel = (function () {
    var novaTarefa = ko.observable('');
    var tarefas = ko.observableArray([]);

    var ordenar = function () {
        tarefas.sort(function (x, y) {
            if (x.terminada() && !y.terminada()) {
                return 1;
            } else if (!x.terminada() && y.terminada()) {
                return -1;
            } else {
                return 0;
            }
        });
    };

    var criarNovaTarefa = function (titulo) {
        if (titulo) {
            var tarefa = criarTarefa({ Terminada: false, Titulo: titulo, Id: 0 });
            return tarefa;
        }
        return null;
    };

    var criarTarefa = function(dados) {
         var tarefa =  new todo.Tarefa()
                .terminada(dados.Terminada)
                .titulo(dados.Titulo)
                .id(dados.Id);
        
         tarefa.terminada.subscribe(function () {
             ordenar();
             todo.atualizar(tarefa);
         });
        
        return tarefa;
    };

    var adicionar = function () {
        if (novaTarefa) {
            var tarefa = criarNovaTarefa(novaTarefa());
            if (tarefa) {
                tarefas.push(tarefa);
                novaTarefa('');
                ordenar();
                
                todo.salvar(tarefa);
            }
        }
    };

    todo.recuperar();
   
    return {
        novaTarefa: novaTarefa,
        tarefas: tarefas,
        adicionarTarefa: adicionar,
        criarTarefa: criarTarefa,
        ordenar : ordenar
    };
})();