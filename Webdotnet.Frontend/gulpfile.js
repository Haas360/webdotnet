var gulp = require('gulp');
var sass = require('gulp-sass');
var uglify = require('gulp-uglify');
var cssnano = require('gulp-cssnano');
const sourcemaps = require('gulp-sourcemaps');
const babel = require('gulp-babel');
const concat = require('gulp-concat');


var source = 'src/';
var dest = '../Webdotnet.Umbraco/assets/';

// Bootstrap scss source
var bootstrapSass = {
    in: './node_modules/bootstrap-sass/'
};

// Bootstrap fonts source
var fonts = {
    in: [source + 'fonts/*.*'],
    out: dest + 'fonts/'
};

// Our scss source folder: .scss files
var scss = {
    in: source + 'scss/main.scss',
    out: dest + 'css/',
    watch: source + 'scss/**/*',
    sassOpts: {
        outputStyle: 'nested',
        precison: 3,
        errLogToConsole: true,
        includePaths: [bootstrapSass.in + 'assets/stylesheets']
    }
};
// copy bootstrap required fonts to dest
gulp.task('fonts', function () {
    return gulp
        .src(fonts.in)
        .pipe(gulp.dest(fonts.out));
});
// compile scss


gulp.task('sass', ['fonts'], function () {
    return gulp.src(scss.in)
        .pipe(sass(scss.sassOpts))
        // .pipe(cssnano())
        .pipe(gulp.dest(scss.out));
});

gulp.task('sassprod', ['fonts'], function () {
    return gulp.src(scss.in)
        .pipe(sass(scss.sassOpts))
        .pipe(cssnano())
        .pipe(gulp.dest(scss.out));
});

gulp.task('js', function () {
    return gulp.src('src/js/*.js')
        .pipe(sourcemaps.init())
        .pipe(babel({
            presets: ['es2015']
        }))
        .pipe(concat('main.js'))
        .pipe(sourcemaps.write('.'))
        .pipe(gulp.dest(dest + 'js/'));
});
gulp.task('jsprod', function () {
    return gulp.src('src/js/*.js')
        // .pipe(sourcemaps.init())
        .pipe(babel({
            presets: ['es2015']
        }))
        .pipe(concat('main.js'))
        .pipe(uglify())
        // .pipe(sourcemaps.write('.'))
        .pipe(gulp.dest(dest + 'js/'));
});

gulp.task('watch', ['sass', 'js'], function () {
    gulp.watch(scss.watch, ['sass']);
    gulp.watch('src/js', ['js']);

});
gulp.task('production', ['jsprod', 'sassprod'], function () {});
// default task
gulp.task('default', ['sass', 'js'], function () {});
