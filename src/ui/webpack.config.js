var path = require('path');

module.exports = {
	watch: true,
	entry: './src/app.js',
	devtool: 'source-map',
	output: {
		path: path.resolve(__dirname, "../server/Microservices/HttpGateway/HttpGatewayApp/wwwroot"),
		filename: 'app.js'
	},
	module: {
		loaders: [{
			test: /.js?$/,
			loader: 'babel-loader',
			exclude: /node_modules/,
			query: {
				presets: ['es2015', 'react']
			}
		}]
	},
};