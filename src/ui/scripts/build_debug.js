var gulp = require("gulp");
var gutil = require("gulp-util");
var clean = require("gulp-clean");
var gulp_webpack = require("gulp-webpack");

var buildWebpack = require("./webpack");

const debug_build_path = "../../server/Microservices/HttpGateway/HttpGatewayApp/wwwroot";

gulp.task("clean_debug", () => gulp.src(debug_build_path).pipe(clean({ force: true })));

gulp.task("build_jsx", ["clean_debug"], () => {
    gulp
        .src("../src/app.jsx")
        .pipe(gulp_webpack(buildWebpack(false)))
        .pipe(gulp.dest(debug_build_path));
});

gulp.task("build_index", ["clean_debug"], () => {
    var buildingFiles = ["../src/index.html"];

    return gulp.
        src(buildingFiles).
        pipe(gulp.dest(debug_build_path));
});

gulp.task("build_resources", ["clean_debug"], () => {
    var buildingFiles = ["../resources/**"];

    return gulp.
        src(buildingFiles).
        pipe(gulp.dest(debug_build_path + "/resources"));
});

gulp.task("default", ["build_index", "build_resources", "build_jsx"], () =>{});