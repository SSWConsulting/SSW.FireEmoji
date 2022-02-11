import * as cp from "child_process";

export default async function predict(commit: string) {
    return new Promise<string>((c, e) => {
        const modelPath = `${__dirname}\\..\\..\\gitmo.mlnet`;
        const command = `${__dirname}\\..\\..\\dlls\\SSW.FireEmoji.Console.exe --commit '${commit}' --model "${modelPath}"`;
        cp.exec(command, (stderr, stdout) => {
            if (stderr) {
                return e(stderr);
            }
            return c(stdout);
        });
    });
}

//TODO: Send back a json object and map it to an Emoji object
interface Emoji {
    emoji: string;
    score: number;
}
