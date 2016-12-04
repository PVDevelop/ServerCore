module.exports = watch => {
    return {
        devtool: "source-map",
        watch: watch,
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
    }
}