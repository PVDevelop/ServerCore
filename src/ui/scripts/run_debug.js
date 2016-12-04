var gulp = require("gulp");
var watch = require('gulp-watch');
var clean = require("gulp-clean");
var watch = require("gulp-watch");
var gulp_webpack = require("gulp-webpack");

var buildWebpack = require("./webpack");

const debug_build_path = "../../server/Microservices/HttpGateway/HttpGatewayApp/wwwroot";

gulp.task("clean_debug", () => gulp.src(debug_build_path).pipe(clean({ force: true })));

gulp.task("watch_jsx", ["clean_debug"], () => {
    gulp
        .src("../src/app.jsx")
        .pipe(gulp_webpack(buildWebpack(true)))
        .pipe(gulp.dest(debug_build_path));
});

gulp.task("watch_index", ["clean_debug"], () => {
    var watchingFiles = ["../src/index.html"];

    return gulp.
        src(watchingFiles).
        pipe(watch(watchingFiles)).
        pipe(gulp.dest(debug_build_path));
});

gulp.task("watch_resources", ["clean_debug"], () => {
    var watchingFiles = ["../resources/**"];

    return gulp.
        src(watchingFiles).
        pipe(watch(watchingFiles)).
        pipe(gulp.dest(debug_build_path + "/resources"));
});

gulp.task("default", ["watch_index", "watch_resources", "watch_jsx"], () =>{});