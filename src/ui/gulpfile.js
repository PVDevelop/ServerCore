var gulp = require("gulp");
var gutil = require("gulp-util");
var clean = require("gulp-clean");
var watch = require("gulp-watch");
var gulp_webpack = require("gulp-webpack");
var path = require('path');

const debug_build_path = "../server/Microservices/HttpGateway/HttpGatewayApp/wwwroot";

// очищаем папку, в которую будем собирать дебажные бинари
gulp.task("clean_debug", () => gulp.src(debug_build_path).pipe(clean({ force: true })));

// копируем в папку с дебажными бинарями необходимые файлы
gulp.task("copy_debug", ["clean_debug"], () => {
	var copiedFiles = ["./src/Index.html"];
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
						loader: 'babel-loader',
						exclude: /node_modules/,
						query: {
							presets: ['es2015', 'react']
						}
					},
					{
						test: /\.css$/,
						loader: 'style-loader!css-loader'
					},
					{
						test: /\.eot(\?v=\d+\.\d+\.\d+)?$/,
						loader: 'file'
					},
					{
						test: /\.(woff|woff2)$/,
						loader: 'url?prefix=font/&limit=5000'
					},
					{
						test: /\.ttf(\?v=\d+\.\d+\.\d+)?$/,
						loader: "url?limit=10000&mimetype=application/octet-stream"
					},
					{
						test: /\.svg(\?v=\d+\.\d+\.\d+)?$/,
						loader: "url?limit=10000&mimetype=image/svg+xml"
					}]
			},
			resolve:
			{
				extensions: ['', '.js', '.jsx']
			}
		}))
		.pipe(gulp.dest(debug_build_path));
});