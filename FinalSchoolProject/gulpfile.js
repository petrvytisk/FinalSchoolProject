const gulp = require('gulp');
const sass = require('gulp-sass')(require('sass'));
const path = require('path');

// Cesty k SCSS a CSS souborům
const paths = {
    scss: './wwwroot/scss/**/*.scss', // Zdrojové SCSS soubory
    css: './wwwroot/css'             // Výstupní CSS složka
};

// Task pro kompilaci SCSS na CSS
gulp.task('sass', function () {
    return gulp.src(paths.scss)         // Zdrojové SCSS soubory
        .pipe(sass().on('error', sass.logError)) // Kompilace SCSS
        .pipe(gulp.dest(paths.css));    // Výstupní CSS složka
});

// Watch task pro sledování změn v SCSS souborech
gulp.task('watch', function () {
    gulp.watch(paths.scss, gulp.series('sass'));
});

// Defaultní task
gulp.task('default', gulp.series('sass', 'watch'));
