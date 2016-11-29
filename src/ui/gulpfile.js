var gulp = require("gulp");
var gutil = require("gulp-util");
var clean = require("gulp-clean");
var watch = require("gulp-watch");
var gulp_webpack = require("gulp-webpack");
var path = require("path");

const debug_build_path = "../server/Microservices/HttpGateway/HttpGatewayApp/wwwroot";

// очищаем папку, в которую будем собирать дебажные бинари
gulp.task("clean_debug", () => gulp.src(debug_build_path).pipe(clean({ force: true })));

// копируем в папку с дебажными бинарями необходимые файлы
gulp.task("copy_debug", ["clean_debug"], () => {
    var copiedFiles = [
        "./src/Index.html"];
        //"./src/theme/**"];
    gulp
        .src(copiedFiles)
        .pipe(gulp.dest(debug_build_path));
});

gulp.task("default", ["copy_debug"], () => {
    gulp
        .src("./src/app.jsx")
        .pipe(gulp_webpack({
            devtool: "source-map",
            watch: true,
            output: {
                filename: "app.js"
            },
            module: {
                loaders: [
                    {
                        test: /\.jsx?$/,
                        loader: "babel-loader",
                        exclude: /node_modules/,
                        query: {
                            presets: ["es2015", "react", "stage-0", "stage-1", "stage-2", "stage-3"]
                        }
                    }]
            },
            resolve:
            {
                extensions: ["", ".js", ".jsx"]
            }
        }))
        .pipe(gulp.dest(debug_build_path));
});