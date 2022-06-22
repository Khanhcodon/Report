

(function (eReport) {

    var commandManager = function () { };
    commandManager.executed = [];
    commandManager.unexecuted = [];

    commandManager.execute = function execute(cmd) {
        cmd.execute();
        commandManager.executed.push(cmd);
    };

    commandManager.undo = function undo() {
        var cmd1 = commandManager.executed.pop();
        if (cmd1 !== undefined) {
            if (cmd1.unexecute !== undefined) {
                cmd1.unexecute();
            }
            commandManager.unexecuted.push(cmd1);
        }
    };

    commandManager.redo = function redo() {
        var cmd2 = commandManager.unexecuted.pop();

        if (cmd2 === undefined) {
            cmd2 = commandManager.executed.pop();
            commandManager.executed.push(cmd2);
            commandManager.executed.push(cmd2);
        }

        if (cmd2 !== undefined) {
            cmd2.execute();
            commandManager.executed.push(cmd2);
        }
    };

    eReport.commandManager = commandManager;
})
(window.eReport = window.eReport || {});