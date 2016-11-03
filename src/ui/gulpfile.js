var gulp = require("gulp");
var gutil = require("gulp-util");
var clean = require("gulp-clean");
var gulp_webpack = require("gulp-webpack");
var path = require('path');

const debug_build_path = "./bin/debug";
//const debug_deploy_path = "./bin/deploy";
const debug_deploy_path = "../server/Microservices/HttpGateway/HttpGatewayApp/wwwroot";

// очищаем папку, в которую будем собирать дебажные бинари
gulp.task("clean_debug", () => gulp.src(debug_build_path).pipe(clean()));

// копируем в папку с дебажными бинарями необходимые файлы
gulp.task("copy_debug", ["clean_debug"], () => {
	var copiedFiles = ["./src/Index.html"];
	gulp
		.src(copiedFiles)
		.pipe(gulp.dest(debug_build_path));
});

// собираем бандл react проекта в папку с дебажными банарями и следить за ним
gulp.task("webpack_debug", ["clean_debug"], () => {
	runWebpack(false);
});

function runWebpack(doWatch)
{
	gulp
		.src("./src/app.jsx")
		.pipe(gulp_webpack({
			devtool: "source-map",
			watch: doWatch,
			output: {
				filename: "app.js"
			},
			module: {
				loaders: [{
					test: /\.jsx?$/,
					loader: 'babel-loader',
					exclude: /node_modules/,
					query: {
						presets: ['es2015', 'react']
					}
				}]
			},
			resolve:
			{
				extensions: ['', '.js', '.jsx']
			}
		}))
		.pipe(gulp.dest(debug_build_path));
}

gulp.task("clean_debug_deploy", () => 
	gulp
		.src(debug_deploy_path)
		.pipe(clean({ force: true })));

gulp.task("copy_debug_deploy", ["clean_debug_deploy", "webpack_debug"], () => {
	gulp
		.src(debug_build_path + "/**")
		.pipe(gulp.dest(debug_deploy_path));
});

gulp.task("default", ["copy_debug", "webpack_debug", "copy_debug_deploy"]);