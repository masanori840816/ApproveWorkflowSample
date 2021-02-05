var path = require('path');

module.exports = {
    mode: 'development',
    entry: {
        'signInPage': './ts/signIn.page.ts',
    },
    module: {
        rules: [
            {
                test: /\.tsx?$/,
                use: 'ts-loader',
                exclude: /node_modules/
            }
        ]
    },
    resolve: {
        extensions: [ '.tsx', '.ts', '.js' ]
    },
    output: {
        filename: '[name].js',
        path: path.resolve(__dirname, 'wwwroot/js'),
        library: 'Page',
        libraryTarget: 'umd'
    }
};